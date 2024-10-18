using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    #region
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("Area"))
    //        return;
    //
    //    Vector3 playerPos = GameManager.Instance.player.transform.position;
    //    Vector3 myPos = transform.position;
    //
    //    
    //    //float diffX = Mathf.Abs(playerPos.x - myPos.x);
    //    //float diffY = Mathf.Abs(playerPos.y - myPos.y);
    //
    //    //Vector3 playerDir = GameManager.Instance.player.inputVec;
    //    //float dirX = playerDir.x < 0 ? -1 : 1;
    //    //float dirY = playerDir.y < 0 ? -1 : 1;
    //    
    //
    //    float tileWidth = 136.53334f; // 타일의 가로 길이
    //    float tileHeight = 31f; // 타일의 세로 길이
    //    
    //    float dirX = playerPos.x - myPos.x;
    //    float dirY = playerPos.y - myPos.y;
    //    
    //    //float diffX = Mathf.Abs(dirX);
    //    //float diffY = Mathf.Abs(dirY);
    //    
    //    dirX = dirX > 0 ? 1 : -1;
    //    dirY = dirY > 0 ? 1 : -1;
    //
    //    switch(transform.tag)
    //    {
    //        case "Ground":
    //
    //            float diffX = playerPos.x - myPos.x; 
    //            float diffY = playerPos.y - myPos.y;
    //            
    //            //float dirX = diffX > 0 ? 1 : -1;
    //            //float dirY = diffY > 0 ? 1 : -1;
    //
    //            diffX = Mathf.Abs(diffX);
    //            diffY = Mathf.Abs(diffY);
    //            
    //
    //            if (diffX > diffY)
    //            {
    //                transform.Translate(dirX * tileWidth, 0, 0/*Vector3.right * dirX * tileWidth*/);
    //                
    //            }
    //            else if(diffX < diffY)
    //            {
    //                transform.Translate(0, dirY * tileHeight, 0/*Vector3.up * dirY * tileHeight*/);
    //                
    //            }
    //            //else if(Mathf.Abs(diffX - diffY) <= 0.001f)
    //            //{
    //            //    //transform.Translate(Vector3.up * dirY * tileHeight);
    //            //    //transform.Translate(Vector3.right * dirX * tileWidth); 
    //            //    transform.Translate(new Vector3(dirX * tileWidth, dirY * tileHeight, 0));
    //            //}
    //            //else
    //            //{
    //            //    //Debug.Log("orth");
    //            //    transform.Translate(dirX * tileWidth, dirY * tileHeight, 0);
    //            //}
    //            break;                
    //        case "Enemy":
    //            break;
    //    }
    //}
    #endregion

    public GameObject[] tiles; //타일 배열
    public GameObject player; 
    private float tileWidth; //타일의 너비    
    private BoxCollider2D triggerCollider; //트리거 콜라이더
    //private BoxCollider2D maptileCollider; //맵 콜라이더

    // Start is called before the first frame update
    void Start()
    {
        // 배열 길이 확인
        if (tiles.Length < 2)
        {
            Debug.LogError("타일 배열에 두 개의 타일이 설정되어 있지 않습니다.");
            return;
        }

        tileWidth = tiles[0].GetComponent<SpriteRenderer>().bounds.size.x; //타일의 너비를 구한다

        triggerCollider = GetComponent<BoxCollider2D>(); //트리거 콜라이더 설정
        if(triggerCollider == null)
        {
            triggerCollider = gameObject.AddComponent<BoxCollider2D>();
            triggerCollider.isTrigger = true;
            triggerCollider.size = new Vector2(tileWidth, triggerCollider.size.y);
            Debug.LogError("트리거 콜라이더가 없습니다ㅠㅠ");            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if(collision.CompareTag("Player"))
        {
            Vector3 playerPosition = player.transform.position;
            if (playerPosition.x < tiles[1].transform.position.x - 5)
            {
                MoveTiles(tiles[0]);
                Debug.Log("0번 타일이 1번 타일 오른쪽으로 이동했습니다.");
            }
            else if(playerPosition.x > tiles[0].transform.position.x + 5)
            {
                MoveTiles(tiles[1]);
                Debug.Log("1번 타일이 0번 타일 오른쪽으로 이동했습니다.");
            }
        }
        #region
        //foreach (GameObject tile in tiles)
        //{           
        //    if (
        //        (maptileCollider.bounds.Contains(playerPosition) && playerPosition.x < tiles[0].transform.position.x - 5 
        //        && collision.CompareTag("Tile0 Collider")) //플레이어가 오른쪽으로 이동하여 0번 타일의 콜라이더와 왼쪽에서 충돌할 때
        //        || 
        //        (maptileCollider.bounds.Contains(playerPosition) && playerPosition.x > tiles[1].transform.position.x + 5 
        //        && collision.CompareTag("Tile1 Collider")) //플레이어가 왼쪽으로 이동하여 1번 타일의 콜라이더와 오른쪽에서 충돌할 때
        //       ) 
        //    {
        //        return;
        //    }
        //    else if(
        //            (maptileCollider.bounds.Contains(playerPosition) && playerPosition.x > tiles[1].transform.position.x - 5 
        //            && collision.CompareTag("Tile1 Collider")) //플레이어가 오른쪽으로 이동하여 1번 타일의 콜라이더와 왼쪽에서 충돌할 때
        //            || 
        //            (maptileCollider.bounds.Contains(playerPosition) && playerPosition.x > tiles[0].transform.position.x + 5 
        //            && collision.CompareTag("Tile0 Collider")) //플레이어가 왼쪽으로 이동하여 0번 타일의 콜라이더와 오른쪽에서 충돌할 때
        //           )
        //    {
        //        MoveTiles(tile);
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
        #endregion
    }

    private void SwapTiles()
    {
        GameObject temp = tiles[0];
        tiles[0] = tiles[1];
        tiles[1] = temp;
    }

    private void MoveTiles(GameObject currentTile)
    {
        if(currentTile == tiles[1])
        {
            tiles[0].transform.position = new Vector3(tiles[1].transform.position.x + tileWidth, tiles[0].transform.position.y, 0);            
        }
        else if(currentTile == tiles[0])
        {
            tiles[1].transform.position = new Vector3(tiles[0].transform.position.x - tileWidth, tiles[0].transform.position.y, 0);            
        }
        SwapTiles();
    }
}
