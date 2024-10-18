using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    public float speed;
    public float currentHP;
    public float MAXHP;
    public Rigidbody2D target;
    
    bool isAlive;

    Rigidbody2D rigid;
    Animator monster_ani;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        monster_ani = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isTimeOnGoing) return;

        if (!isAlive)  return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.isTimeOnGoing) return;

        spriter.flipX = target.position.x > rigid.position.x;
    }

    private void OnEnable() //생성(활성화)되고 나서 플레이어를 쫓아갈 수 있도록
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isAlive = true;
        currentHP = MAXHP;
        spriter.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponControl_HolyBook holyBook;
        WeaponControl_MagicVolt magicVolt;
        WeaponControl_Whip Whip;
        WeaponControl_Knife Knife;

        collision.TryGetComponent(out holyBook);
        collision.TryGetComponent(out magicVolt);
        collision.TryGetComponent(out Whip);
        collision.TryGetComponent(out Knife);

        if (!collision.CompareTag("Bullet") || !isAlive) return;
        
        if (holyBook != null) currentHP -= holyBook.damage;
        if (magicVolt != null) currentHP -= magicVolt.damage;
        if (Whip != null) currentHP -= Whip.damage;
        if (Knife != null) currentHP -= Knife.damage;
        
        if (currentHP > 0)
        {
            StopCoroutine("HitColor_Action_co");
            StartCoroutine("HitColor_Action_co");
            StartCoroutine("KnockBack");
            AudioManager.instance.PlaySFX(AudioManager.Sfx.sfx_enemyHit);
        }
        else
        {            
            Die();
            GameManager.Instance.GetExp();
            GameManager.Instance.kill++;            
        }
    }

    public void Die()
    {
        monster_ani.SetTrigger("Die");        
        isAlive = false;
    }

    public void Monster_Disable()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator HitColor_Action_co() // 몬스터가 맞을 때(피격 시) 색깔 바뀌게
    {
        spriter.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriter.color = Color.white;
    }

    private IEnumerator KnockBack()
    {
        yield return wait; //다음 하나의 물리 프레임을 기다리는 딜레이
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse); //ForceMode2D.Impulse -> 즉발적인 힘
    }
}
