using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl_MagicVolt : MonoBehaviour
{
    public float damage;
    public int pen = 0;

    Rigidbody2D rigid; //탄환에 물리 충돌 구현  

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();        
    }

    public void Init(ItemData data /*float damage, int pen*/, UnityEngine.Vector3 dir)
    {        
        //this.damage = damage;
        damage = data.baseDamage;
        this.pen = pen;

        pen = 0;

        if (pen > -1)
        {
            rigid.velocity = dir * 8f;
        }
        else
        {
            Debug.Log("wow");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision);

        if (!collision.CompareTag("Monster") || pen == -1) 
            return;

        pen--;

        if(pen == -1)
        {
            //오브젝트 풀링 재활용을 위한 비활성화
            rigid.velocity = Vector2.zero;            
            gameObject.SetActive(false);
        }

        /*
        if (collision.CompareTag("Area"))
            Debug.Log(collision);
        gameObject.SetActive(false);
        */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Area"))
        {
            Debug.Log(collision);
            gameObject.SetActive(false);
        }
    }
}
