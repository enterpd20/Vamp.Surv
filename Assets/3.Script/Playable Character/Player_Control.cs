using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    private Animator Player_ani; // Sprite 애니메이션
    bool isMove = false;
        
    private float _maxHP = 350f; // 체력
    private float _currentHP;

    public Player_MonsterScanner scanner;

    public float MAXHP => _maxHP; // 최대 체력
    public float CurrentHP => _currentHP; // 현재 체력
    public Vector3 LastMoveDir;  //새로추가

    private SpriteRenderer renderer;

    private Movement2D movement_2D; // 움직임
    

    
    private void Awake()
    {        
        movement_2D = transform.GetComponent<Movement2D>();
        if (movement_2D == null)
        {
            Debug.LogError("Movement2D 컴포넌트가 없습니다!");
        }
        _currentHP = MAXHP;

        TryGetComponent(out renderer);
        TryGetComponent(out Player_ani);
        scanner = transform.GetComponent<Player_MonsterScanner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (movement_2D.move_Speed <= 0f)
        {
            movement_2D.move_Speed = 5f; //캐릭터 이동속도
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isTimeOnGoing) return;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //movement_2D.MoveTo(new Vector3(x, y, 0));

        Vector3 moveDir = new Vector3(x, y, 0);//새로추가
        movement_2D.MoveTo(moveDir); //새로추가

        if(moveDir != Vector3.zero) //새로추가
        {
            LastMoveDir = moveDir.normalized;
            
        }

        //if(x < 0 || y < 0)
        //{
        //    renderer.flipX = true;
        //    Player_ani.SetBool("isMove", true);
        //}
        //else if (x > 0 || y >0)
        //{
        //    renderer.flipX = false;
        //    Player_ani.SetBool("isMove", true);
        //}    
        //else
        //{
        //    Player_ani.SetBool("isMove", false);
        //    isMove = false;
        //}

        if (x < 0)
        {
            renderer.flipX = true;
            Player_ani.SetBool("isMove", true);
        }
        else if (x > 0)
        {
            renderer.flipX = false;
            Player_ani.SetBool("isMove", true);
        }
        else if (y != 0)
        {
            Player_ani.SetBool("isMove", true);
        }
        else
        {
            Player_ani.SetBool("isMove", false);
            isMove = false;
        }
    }    

    public void isStopping()
    {
        isMove = false;
    }

    public void TakeDamage(float damage)
    {
        _currentHP -= damage;
        Debug.Log("PlayerHP : " + CurrentHP);

        StopCoroutine("HitColor_Action_co");
        StartCoroutine("HitColor_Action_co");

        
    }

    private IEnumerator HitColor_Action_co() // 플레이어가 맞을 때(피격 시) 색깔 바뀌게
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.color = Color.white;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isTimeOnGoing) return;

        GameManager.Instance.currentHP -= Time.deltaTime * 10;
        StopCoroutine("HitColor_Action_co");
        StartCoroutine("HitColor_Action_co");

        if (GameManager.Instance.currentHP < 0)
        {
            for(int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            Player_ani.SetTrigger("Die");
            GameManager.Instance.GameOver();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
