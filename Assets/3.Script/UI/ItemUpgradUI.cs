using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUpgradUI : MonoBehaviour
{
    public ItemData data;
    public int level;
    public WeaponManager weapon;
    
    Image icon;
    Text ItemLevel;
    Text ItemName;
    Text ItemDescription;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        ItemLevel = texts[0];
        ItemName = texts[1];
        ItemDescription = texts[2];

        ItemName.text = data.itemName;
    }

    private void OnEnable()
    {
        ItemLevel.text = "Lv." + level;

        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                ItemDescription.text 
                    = string.Format(data.itemDescription, data.damage_Enhancing[level] * 100, data.count_Enhancing[level]);
                break;
            default:
                ItemDescription.text = string.Format(data.itemDescription);
                break;
        }
    }

    /*
    private void LateUpdate()
    {
        ItemLevel.text = "Lv." + level;
    }
    */

    public void OnClick()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.Melee:                
            case ItemData.ItemType.Range:
                if(level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<WeaponManager>();                    
                    weapon.Init(data);                    
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damage_Enhancing[level];
                    nextCount += data.count_Enhancing[level];

                    weapon.WeaponLevelUp(nextDamage, nextCount);
                }
                level++;
                break;

            case ItemData.ItemType.Heal:
                GameManager.Instance.currentHP = GameManager.Instance.MAXHP;
                AudioManager.instance.PlaySFX(AudioManager.Sfx.sfx_sounds_heal);
                break;
        }
        
        if(level == data.damage_Enhancing.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
