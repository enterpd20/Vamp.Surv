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
    //    float tileWidth = 136.53334f; // Ÿ���� ���� ����
    //    float tileHeight = 31f; // Ÿ���� ���� ����
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

    public GameObject[] tiles; //Ÿ�� �迭
    public GameObject player; 
    private float tileWidth; //Ÿ���� �ʺ�    
    private BoxCollider2D triggerCollider; //Ʈ���� �ݶ��̴�
    //private BoxCollider2D maptileCollider; //�� �ݶ��̴�

    // Start is called before the first frame update
    void Start()
    {
        // �迭 ���� Ȯ��
        if (tiles.Length < 2)
        {
            Debug.LogError("Ÿ�� �迭�� �� ���� Ÿ���� �����Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        tileWidth = tiles[0].GetComponent<SpriteRenderer>().bounds.size.x; //Ÿ���� �ʺ� ���Ѵ�

        triggerCollider = GetComponent<BoxCollider2D>(); //Ʈ���� �ݶ��̴� ����
        if(triggerCollider == null)
        {
            triggerCollider = gameObject.AddComponent<BoxCollider2D>();
            triggerCollider.isTrigger = true;
            triggerCollider.size = new Vector2(tileWidth, triggerCollider.size.y);
            Debug.LogError("Ʈ���� �ݶ��̴��� �����ϴ٤Ф�");            
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
                Debug.Log("0�� Ÿ���� 1�� Ÿ�� ���������� �̵��߽��ϴ�.");
            }
            else if(playerPosition.x > tiles[0].transform.position.x + 5)
            {
                MoveTiles(tiles[1]);
                Debug.Log("1�� Ÿ���� 0�� Ÿ�� ���������� �̵��߽��ϴ�.");
            }
        }
        #region
        //foreach (GameObject tile in tiles)
        //{           
        //    if (
        //        (maptileCollider.bounds.Contains(playerPosition) && playerPosition.x < tiles[0].transform.position.x - 5 
        //        && collision.CompareTag("Tile0 Collider")) //�÷��̾ ���������� �̵��Ͽ� 0�� Ÿ���� �ݶ��̴��� ���ʿ��� �浹�� ��
        //        || 
        //        (maptileCollider.bounds.Contains(playerPosition) && playerPosition.x > tiles[1].transform.position.x + 5 
        //        && collision.CompareTag("Tile1 Collider")) //�÷��̾ �������� �̵��Ͽ� 1�� Ÿ���� �ݶ��̴��� �����ʿ��� �浹�� ��
        //       ) 
        //    {
        //        return;
        //    }
        //    else if(
        //            (maptileCollider.bounds.Contains(playerPosition) && playerPosition.x > tiles[1].transform.position.x - 5 
        //            && collision.CompareTag("Tile1 Collider")) //�÷��̾ ���������� �̵��Ͽ� 1�� Ÿ���� �ݶ��̴��� ���ʿ��� �浹�� ��
        //            || 
        //            (maptileCollider.bounds.Contains(playerPosition) && playerPosition.x > tiles[0].transform.position.x + 5 
        //            && collision.CompareTag("Tile0 Collider")) //�÷��̾ �������� �̵��Ͽ� 0�� Ÿ���� �ݶ��̴��� �����ʿ��� �浹�� ��
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
