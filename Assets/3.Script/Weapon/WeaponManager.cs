using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    //public int WepaonLevel; //무기 레벨에 따른 대미지 상승, 기믹 추가

    float timer;
    Player_Control player;

    /**/Vector2 lastInputDirection; // 마지막 입력 방향을 저장할 변수

    private void Awake()
    {
        //player = GetComponentInParent<Player_Control>();
        player = GameManager.Instance.player;
    }

    /*
    private void Start()
    {
        Init();
        
        //InvokeRepeating("Init", 0f, 1f); 
        //Init()을 1초마다 지속적으로 호출, 계속 겹쳐진다 // 0f = 0초부터 시작 1f = 간격 
        //1f, 3f라고 한다면 1초부터 시작해서 3초간격으로 호출
    }
    */

    void Update()
    {
        if (!GameManager.Instance.isTimeOnGoing) return;

        /**/ // 마지막 입력 방향 저장        
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        /**/if(inputDirection != Vector2.zero)
        /**/{
        /**/    lastInputDirection = inputDirection.normalized;
        /**/}

        switch (id)
        {
            case 0: //성경                
                transform.Rotate(Vector3.back * speed * Time.deltaTime); //성경 회전시키기                
                break;
            case 1: //채찍
                //휘두르는 간격?
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Whip_Slash();
                }
                break;
            case 2: //매직완드, 아래 Init에서 이어져서, 0.7초마다 매직볼트가 발사되게 함
                timer += Time.deltaTime;
                if(timer > speed)
                {
                    timer = 0f;
                    MagicVolt_Fire();
                }
                break;
            case 3: //단검
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Knife_Fire();
                }
                break;
        }
    }
        

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemID;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property set
        id = data.itemID;
        damage = data.baseDamage;
        count = data.baseCount;    

        for (int i = 0; i < GameManager.Instance.poolManager.Prefabs.Length; i++ )
        {
            if(data.projectile == GameManager.Instance.poolManager.Prefabs[i])
            {
                prefabId = i;            
                break;
            }
        }

        switch (id)
        {
            case 0: //성경
                speed = 150;
                HolyBook_Rotation(data);
                break;
            case 1: //채찍
                //공격간격
                speed = 0.5f;
                break;
            case 2: //매직완드
                speed = 1.0f; //투사체 발사 간격
                break;
            case 3: //단검
                speed = 0.7f;
                break;
        }
    }
   

    public void HolyBook_Rotation(ItemData data)
    {
        for(int i = 0; i < count; i++)
        {
            Transform book;

            /**/if(i < transform.childCount)
            {
                book = transform.GetChild(i);
            }
            else
            {
                book = GameManager.Instance.poolManager.Get(prefabId).transform;
                book.parent = transform;
            }

            //실행중에는 프리팹에서 가져와 Weapon-000의 자식 오브젝트로 만들기
            //book = GameManager.Instance.poolManager.Get(prefabId).transform;
            //book.parent = transform;

            /**/
            book.localPosition = Vector3.zero;
            /**/
            book.localRotation = Quaternion.identity;

            //성경 회전하도록 배치
            Vector3 rotVec = Vector3.back * 360 * i / count;
            ///**/transform.localRotation = Quaternion.identity; //성경의 초기 회전 위치 설정 240622 16:52
            book.Rotate(rotVec);
            book.Translate(book.up * 1.5f, Space.World); //y축으로 1.5만큼 올리기
                        
            //book.GetComponent<WeaponControl_HolyBook>().Init(data /*damage, -1*/); // -1은 무한 관통
            book.GetComponent<WeaponControl_HolyBook>().Init(data);
        }
    }

    public void WeaponLevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
        {
            ItemData data = GameManager.Instance.GetItemDataById(id); // id에 해당하는 ItemData 객체를 가져옵니다.
            if (data != null)
            {
                HolyBook_Rotation(data); // ItemData 객체를 전달하여 HolyBook_Rotation 메서드를 호출합니다.
            }
            else
            {
                Debug.LogError("ItemData is null");
            }
        }
            
    }


    public void Whip_Slash()
    {
        Transform whip = GameManager.Instance.poolManager.Get(prefabId).transform;

        //whip.GetComponent<WeaponControl_Whip>().SaveDirection(whip);
        //whipControl.RestoreDirection(whip);

        whip.SetParent(player.transform, false);

        /**/float direction = player.transform.localScale.x > 0 ? 1 : -1;
        //whip.localScale = new Vector3(direction, whip.localScale.y, whip.localScale.z);

        bool playerflip = player.transform.GetComponent<SpriteRenderer>().flipX;
        float offsetX = playerflip ? -3 : 3;

        whip.position = new Vector3(player.transform.position.x + offsetX, player.transform.position.y);        
        
        //whip.rotation = player.transform.rotation;

        ///**/Vector3 whipDirection = player.transform.localScale.x > 0 ? Vector3.right : Vector3.left;
        //whip.rotation = Quaternion.LookRotation(whipDirection);
               
        WeaponControl_Whip whipControl = whip.GetComponent<WeaponControl_Whip>();
        whipControl.Init(/*damage, -1*/ GameManager.Instance.GetItemDataById(id));       
        whipControl.Attack();

        AudioManager.instance.PlaySFX(AudioManager.Sfx.sfx_projectile_whip);
    }

    public void MagicVolt_Fire()
    {
        if (!player.scanner.nearestTarget)
        {
            //Debug.Log("targeting error");
            return;        
        }

        //투사체가 날아갈 방향
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position; //투사체가 날아갈 방향(크기까지 포함됨)        
        dir = dir.normalized;
        

        Transform magicvolt = GameManager.Instance.poolManager.Get(prefabId).transform;
        magicvolt.position = transform.position; //투사체가 날아갈 위치
        magicvolt.rotation = Quaternion.FromToRotation(Vector3.up, dir); //투사체가 날아가는 쪽으로 회전

        // WeaponControl_MagicVolt의 Init에게 전달
        magicvolt.GetComponent<WeaponControl_MagicVolt>().Init(GameManager.Instance.GetItemDataById(id), dir);
        //ItemData data = GameManager.Instance.GetItemDataById(id);

        AudioManager.instance.PlaySFX(AudioManager.Sfx.sfx_projectile_magicVolt);
    }    

    public void Knife_Fire() //단검
    {
        Transform knife = GameManager.Instance.poolManager.Get(prefabId).transform;
        knife.position = transform.position;

        if (lastInputDirection != Vector2.zero)
        {                        
            ///**/knife.SetParent(player.transform, false);
            //bool playerflip = player.transform.GetComponent<SpriteRenderer>().flipX;

            /**/knife.rotation = Quaternion.FromToRotation(Vector3.right, lastInputDirection);                        
            
            //knifeControl.GetComponent<WeaponControl_Knife>().Init(damage, count, dir);
        }

        knife.GetComponent<WeaponControl_Knife>().
            Init(/*damage, count*/ GameManager.Instance.GetItemDataById(id), lastInputDirection);

        AudioManager.instance.PlaySFX(AudioManager.Sfx.sfx_projectile_knife);
    }
}
