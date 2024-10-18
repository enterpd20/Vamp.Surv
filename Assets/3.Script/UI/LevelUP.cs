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
        //1. 모든 아이템 비활성화
        foreach(ItemUpgradUI item in items)
        {
            item.gameObject.SetActive(false);
        }

        //2. 그 중에서 랜덤하게 3개 아이템만 활성화
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
            //3. 만렙 아이템의 경우에는 안나오게
            if(ranItem.level == ranItem.data.damage_Enhancing.Length)
            {
                //더이상 안나오게
                continue;
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
            */

            if (ranItem.level == ranItem.data.damage_Enhancing.Length && ranItem.data.itemType != ItemData.ItemType.Heal)
            {
                //더이상 안나오게
                continue; // 최고 레벨에 도달한 아이템은 건너뜁니다. Heal 아이템은 예외
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }

        }
    }
}
