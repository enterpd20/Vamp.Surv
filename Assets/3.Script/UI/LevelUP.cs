using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUP : MonoBehaviour
{
    RectTransform rect;
    ItemUpgradUI[] items;

    // Start is called before the first frame update
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<ItemUpgradUI>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.Instance.TimeStop();
        AudioManager.instance.PlaySFX(AudioManager.Sfx.sfx_levelup);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.Instance.TimeOnGoing();
        AudioManager.instance.EffectBgm(false);
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        //1. ��� ������ ��Ȱ��ȭ
        foreach(ItemUpgradUI item in items)
        {
            item.gameObject.SetActive(false);
        }

        //2. �� �߿��� �����ϰ� 3�� �����۸� Ȱ��ȭ
        int[] ran = new int[3];
        while(true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            /*
            if ((ran[0] != ran[1]) && (ran[1] != ran[2]) && (ran[0] != ran[2]))
                break;
            */

            if ((ran[0] != ran[1] && (ran[1] != ran[2] && (ran[0] != ran[2]) &&
            (items[ran[0]].level < items[ran[0]].data.damage_Enhancing.Length || items[ran[0]].data.itemType == ItemData.ItemType.Heal &&
            items[ran[1]].level < items[ran[1]].data.damage_Enhancing.Length || items[ran[1]].data.itemType == ItemData.ItemType.Heal &&
            items[ran[2]].level < items[ran[2]].data.damage_Enhancing.Length || items[ran[2]].data.itemType == ItemData.ItemType.Heal))))
            {
                break;
            }
        }

        for(int i = 0; i < ran.Length; i++)
        {
            ItemUpgradUI ranItem = items[ran[i]];

            /*
            //3. ���� �������� ��쿡�� �ȳ�����
            if(ranItem.level == ranItem.data.damage_Enhancing.Length)
            {
                //���̻� �ȳ�����
                continue;
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
            */

            if (ranItem.level == ranItem.data.damage_Enhancing.Length && ranItem.data.itemType != ItemData.ItemType.Heal)
            {
                //���̻� �ȳ�����
                continue; // �ְ� ������ ������ �������� �ǳʶݴϴ�. Heal �������� ����
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }

        }
    }
}
