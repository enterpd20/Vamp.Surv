using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] Prefabs; //프리팹을 저장할 변수
    List<GameObject>[] Pools; //풀을 담당하는 리스트

    /**/public float spawnInterval = 5f; // 몬스터 스폰 간격
    /**/private float spawnTimer; // 타이머
    /**/private int currentIndex = 0; // 현재 스폰될 몬스터의 인덱스

    private void Awake()
    {
        Pools = new List<GameObject>[Prefabs.Length];

        for(int i = 0; i < Pools.Length; i++)
        {
            Pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index) //풀링 역할을 수행
    {
        if(index < 0 || index >= Pools.Length)
        {
            Debug.LogError("Pool index out of range");
            return null;
        }

        GameObject select = null;

        //선택한 풀의 비활성화된 게임오브젝트에 접근
        foreach(GameObject item in Pools[index])
        {
            if(!item.activeSelf)
            {
                //발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //못찾았다면? 
        if (/*!select*/ select == null)
        {
            //새롭게 생성하고 select 변수에 할당
            select = Instantiate(Prefabs[index], transform);
            Pools[index].Add(select);
            //Debug.Log("Instantiating new monster with index: " + index);
        }
        return select;
    }



    void SpawnMonster()
    {
        // 0번부터 22번 목록의 몬스터를 순차적으로 스폰하도록
        GameObject monster = Get(currentIndex);
        if (monster != null)
        {
            monster.SetActive(true);
            currentIndex = (currentIndex + 1) % 23; // currentIndex를 증가시키고, 23을 나누어 0부터 22까지 순회
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnMonster();
            spawnTimer = 0f; // 타이머 리셋
        }
    }

}
