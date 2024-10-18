using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MonsterSpawnArea : MonoBehaviour
{
    public Transform[] spawnPoint;
    //public SpawnData[] spawnData;

    int gameLevel;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isTimeOnGoing) return;

        timer += Time.deltaTime;
        //gameLevel = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / 10f)); //Mathf 클래스의 FloorToInt -> 소수점 아래 버림

        /**/gameLevel = Mathf.FloorToInt(GameManager.Instance.gameTime / 10f);
        /**/gameLevel = Mathf.Clamp(gameLevel, 0, GameManager.Instance.poolManager.Prefabs.Length - 1);
        //Debug.Log(gameLevel);

        if(timer > (gameLevel == 0 ? 0.5f : 0.2f))
        {            
            timer = 0;
            Spawn();
        }        
    }

    void Spawn()
    {
        GameObject Monster = GameManager.Instance.poolManager.Get(gameLevel);
        if (Monster != null)
        {
            Monster.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            ///**/    Debug.Log("Spawned monster at position: " + Monster.transform.position);
            ///**/}
            ///**/else
            ///**/{
            ///**/    Debug.LogWarning("No monster spawned, GameObject is null.");
            ///**/}
        }
    }
}
