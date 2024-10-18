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

        Vector3 playerPos = GameManager.Instance.player.transform.position; //플레이어의 위치
        Vector3 myPos = transform.position; //몬스터의 위치
        Vector3 playerDir = GameManager.Instance.player.GetComponent<Player_Control>().LastMoveDir; //새로추가, 플레이어의 방향

        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffX = Mathf.Abs(dirX); //(플레이어 위치 - 몬스터 위치)의 x축 절대값
        float diffY = Mathf.Abs(dirY); //(플레이어 위치 - 몬스터 위치)의 y축 절대값
                
        dirX = dirX < 0 ? -1 : 1;
        dirY = dirY < 0 ? -1 : 1;

        switch(transform.tag)
        {
            case "Monster":
                if(collision.enabled)
                {
                    if(playerDir == Vector3.zero) //새로추가
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
