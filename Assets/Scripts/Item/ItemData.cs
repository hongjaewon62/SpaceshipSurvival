using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewItem", menuName ="ItemData/Item")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Basic, Bomb, Regeneration, Speed, Power, Cooldown, Heal, Shield, Assistance, GuidedAttack, ElectricityBall, MaxHealth}

    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    public GameObject projectile;
}
