using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterReposition : MonoBehaviour
{
    //Collider2D coll;
    //
    //private void Awake()
    //{
    //    coll = GetComponent<Collider2D>();
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.Instance.player.transform.position; //�÷��̾��� ��ġ
        Vector3 myPos = transform.position; //������ ��ġ
        Vector3 playerDir = GameManager.Instance.player.GetComponent<Player_Control>().LastMoveDir; //�����߰�, �÷��̾��� ����

        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffX = Mathf.Abs(dirX); //(�÷��̾� ��ġ - ���� ��ġ)�� x�� ���밪
        float diffY = Mathf.Abs(dirY); //(�÷��̾� ��ġ - ���� ��ġ)�� y�� ���밪
                
        dirX = dirX < 0 ? -1 : 1;
        dirY = dirY < 0 ? -1 : 1;

        switch(transform.tag)
        {
            case "Monster":
                if(collision.enabled)
                {
                    if(playerDir == Vector3.zero) //�����߰�
                    {
                        playerDir = new Vector3(dirX, dirY, 0).normalized;
                    }
                    transform.Translate(playerDir * 25 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
