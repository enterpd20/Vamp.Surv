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
    //public int WepaonLevel; //���� ������ ���� ����� ���, ��� �߰�

    float timer;
    Player_Control player;

    /**/Vector2 lastInputDirection; // ������ �Է� ������ ������ ����

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
        //Init()�� 1�ʸ��� ���������� ȣ��, ��� �������� // 0f = 0�ʺ��� ���� 1f = ���� 
        //1f, 3f��� �Ѵٸ� 1�ʺ��� �����ؼ� 3�ʰ������� ȣ��
    }
    */

    void Update()
    {
        if (!GameManager.Instance.isTimeOnGoing) return;

        /**/ // ������ �Է� ���� ����        
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        /**/if(inputDirection != Vector2.zero)
        /**/{
        /**/    lastInputDirection = inputDirection.normalized;
        /**/}

        switch (id)
        {
            case 0: //����                
                transform.Rotate(Vector3.back * speed * Time.deltaTime); //���� ȸ����Ű��                
                break;
            case 1: //ä��
                //�ֵθ��� ����?
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Whip_Slash();
                }
                break;
            case 2: //�����ϵ�, �Ʒ� Init���� �̾�����, 0.7�ʸ��� ������Ʈ�� �߻�ǰ� ��
                timer += Time.deltaTime;
                if(timer > speed)
                {
                    timer = 0f;
                    MagicVolt_Fire();
                }
                break;
            case 3: //�ܰ�
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
            case 0: //����
                speed = 150;
                HolyBook_Rotation(data);
                break;
            case 1: //ä��
                //���ݰ���
                speed = 0.5f;
                break;
            case 2: //�����ϵ�
                speed = 1.0f; //����ü �߻� ����
                break;
            case 3: //�ܰ�
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

            //�����߿��� �����տ��� ������ Weapon-000�� �ڽ� ������Ʈ�� �����
            //book = GameManager.Instance.poolManager.Get(prefabId).transform;
            //book.parent = transform;

            /**/
            book.localPosition = Vector3.zero;
            /**/
            book.localRotation = Quaternion.identity;

            //���� ȸ���ϵ��� ��ġ
            Vector3 rotVec = Vector3.back * 360 * i / count;
            ///**/transform.localRotation = Quaternion.identity; //������ �ʱ� ȸ�� ��ġ ���� 240622 16:52
            book.Rotate(rotVec);
            book.Translate(book.up * 1.5f, Space.World); //y������ 1.5��ŭ �ø���
                        
            //book.GetComponent<WeaponControl_HolyBook>().Init(data /*damage, -1*/); // -1�� ���� ����
            book.GetComponent<WeaponControl_HolyBook>().Init(data);
        }
    }

    public void WeaponLevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
        {
            ItemData data = GameManager.Instance.GetItemDataById(id); // id�� �ش��ϴ� ItemData ��ü�� �����ɴϴ�.
            if (data != null)
            {
                HolyBook_Rotation(data); // ItemData ��ü�� �����Ͽ� HolyBook_Rotation �޼��带 ȣ���մϴ�.
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

        //����ü�� ���ư� ����
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position; //����ü�� ���ư� ����(ũ����� ���Ե�)        
        dir = dir.normalized;
        

        Transform magicvolt = GameManager.Instance.poolManager.Get(prefabId).transform;
        magicvolt.position = transform.position; //����ü�� ���ư� ��ġ
        magicvolt.rotation = Quaternion.FromToRotation(Vector3.up, dir); //����ü�� ���ư��� ������ ȸ��

        // WeaponControl_MagicVolt�� Init���� ����
        magicvolt.GetComponent<WeaponControl_MagicVolt>().Init(GameManager.Instance.GetItemDataById(id), dir);
        //ItemData data = GameManager.Instance.GetItemDataById(id);

        AudioManager.instance.PlaySFX(AudioManager.Sfx.sfx_projectile_magicVolt);
    }    

    public void Knife_Fire() //�ܰ�
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
