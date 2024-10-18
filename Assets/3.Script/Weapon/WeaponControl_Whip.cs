using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl_Whip : MonoBehaviour
{
    public float damage;
    public int pen;
    public float cooldown = 0.1f;
    /**/private Vector3 saveDirection;
    /**/public Transform player;

    /**/private SpriteRenderer renderer;

    private void Awake()
    {
        //gameObject.SetActive(false);        
        
        //float direction = GameManager.Instance.player.transform.localScale.x > 0 ? 1 : -1;
        //transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);

    }

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.transform;        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<SpriteRenderer>().flipX = //채찍 스프라이트의 방향
            player.GetComponent<SpriteRenderer>().flipX; //플레이어 스프라이트의 방향
    }


    public void Init(ItemData data /*float damage, int pen*/)
    {
        //this.damage = damage;
        this.damage = data.baseDamage;
        //this.pen = pen;
        this.pen = -1;
    }

    private IEnumerator WhipSlashAction()
    {
       
        gameObject.SetActive(true);
        yield return new WaitForSeconds(cooldown);
        gameObject.SetActive(false);
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return;

        MonsterControl monster = collision.GetComponent<MonsterControl>();
        if(monster != null)
        {
            
        }
    }

    public void Attack()
    {        
        if(gameObject.activeSelf)
        { 
            StopCoroutine("WhipSlashAction");        
            StartCoroutine("WhipSlashAction");
        }
    }
}
