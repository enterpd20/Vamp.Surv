using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName ="Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Heal };

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemID;
    public string itemName;
    [TextArea]
    public string itemDescription;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damage_Enhancing;
    public int[] count_Enhancing;

    [Header("# Weapon Data")]
    public GameObject projectile; //Åõ»çÃ¼ °¹¼ö

}
