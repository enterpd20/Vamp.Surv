using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl_Knife : MonoBehaviour
{
    public float damage;
    public int pen;

    Rigidbody2D rigid; //칼날 물리 충돌 구현

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); 
    }

    public void Init(ItemData data /*float damage, int pen*/, Vector3 dir)
    {
        //this.damage = damage;
        this.damage = data.baseDamage;
        this.pen = pen;

        pen = 0;

        if (pen > -1)
        {
            rigid.velocity = dir * 8f;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster") || pen == -1)
            return;

        pen--;        

        if (pen == -1)
        {
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
