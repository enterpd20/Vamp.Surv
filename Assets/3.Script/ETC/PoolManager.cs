using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] Prefabs; //�������� ������ ����
    List<GameObject>[] Pools; //Ǯ�� ����ϴ� ����Ʈ

    /**/public float spawnInterval = 5f; // ���� ���� ����
    /**/private float spawnTimer; // Ÿ�̸�
    /**/private int currentIndex = 0; // ���� ������ ������ �ε���

    private void Awake()
    {
        Pools = new List<GameObject>[Prefabs.Length];

        for(int i = 0; i < Pools.Length; i++)
        {
            Pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index) //Ǯ�� ������ ����
    {
        if(index < 0 || index >= Pools.Length)
        {
            Debug.LogError("Pool index out of range");
            return null;
        }

        GameObject select = null;

        //������ Ǯ�� ��Ȱ��ȭ�� ���ӿ�����Ʈ�� ����
        foreach(GameObject item in Pools[index])
        {
            if(!item.activeSelf)
            {
                //�߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //��ã�Ҵٸ�? 
        if (/*!select*/ select == null)
        {
            //���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(Prefabs[index], transform);
            Pools[index].Add(select);
            //Debug.Log("Instantiating new monster with index: " + index);
        }
        return select;
    }



    void SpawnMonster()
    {
        // 0������ 22�� ����� ���͸� ���������� �����ϵ���
        GameObject monster = Get(currentIndex);
        if (monster != null)
        {
            monster.SetActive(true);
            currentIndex = (currentIndex + 1) % 23; // currentIndex�� ������Ű��, 23�� ������ 0���� 22���� ��ȸ
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnMonster();
            spawnTimer = 0f; // Ÿ�̸� ����
        }
    }

}
