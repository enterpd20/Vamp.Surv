using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] WeaponPrefabs; //무기 프리팹을 저장할 변수
    List<GameObject>[] WeaponPools; //몬스터 풀을 담당하는 리스트

    private void Awake()
    {
        WeaponPools = new List<GameObject>[WeaponPrefabs.Length];

        for (int i = 0; i < WeaponPools.Length; i++)
        {
            WeaponPools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index) //풀링 역할을 수행
    {
        GameObject select = null;

        //선택한 풀의 비활성화된 게임오브젝트에 접근
        foreach (GameObject item in WeaponPools[index])
        {
            if (!item.activeSelf)
            {
                //발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //못찾았다면? 
        if (!select)
        {
            //새롭게 생성하고 select 변수에 할당
            select = Instantiate(WeaponPrefabs[index], transform);
            WeaponPools[index].Add(select);
        }
        return select;
    }
}
