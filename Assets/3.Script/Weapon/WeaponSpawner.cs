using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] WeaponPrefabs; //���� �������� ������ ����
    List<GameObject>[] WeaponPools; //���� Ǯ�� ����ϴ� ����Ʈ

    private void Awake()
    {
        WeaponPools = new List<GameObject>[WeaponPrefabs.Length];

        for (int i = 0; i < WeaponPools.Length; i++)
        {
            WeaponPools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index) //Ǯ�� ������ ����
    {
        GameObject select = null;

        //������ Ǯ�� ��Ȱ��ȭ�� ���ӿ�����Ʈ�� ����
        foreach (GameObject item in WeaponPools[index])
        {
            if (!item.activeSelf)
            {
                //�߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //��ã�Ҵٸ�? 
        if (!select)
        {
            //���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(WeaponPrefabs[index], transform);
            WeaponPools[index].Add(select);
        }
        return select;
    }
}
