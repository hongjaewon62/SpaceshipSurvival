using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemLevelUp : MonoBehaviour
{
    public ItemData itemData;
    public int level;
    [SerializeField]
    private BasicWeapon basicWeapon;
    [SerializeField]
    private BoomWeapon boomWeapon;
    [SerializeField]
    private ItemAbilityIncrease abilityIncrease;
    [SerializeField]
    private ElectricityBallWeapon electricityBallWeapon;
    [SerializeField]
    private Shield shield;

    private Transform iconPanel;
    private Image iconImage;
    private TextMeshProUGUI levelText;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemDesc;

    private void Awake()
    {
        iconPanel = GetComponentsInChildren<Transform>()[2];
        iconImage = iconPanel.GetComponentsInChildren<Image>()[0];
        iconImage.sprite = itemData.itemIcon;
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        levelText = texts[0];
        itemName = texts[1];
        itemDesc = texts[2];

        itemName.text = itemData.itemName;
    }

    private void OnEnable()
    {
        levelText.text = "LV." + (level);

        switch(itemData.itemType)
        {
            case ItemData.ItemType.Basic:
            case ItemData.ItemType.Bomb:
            case ItemData.ItemType.Regeneration:
            case ItemData.ItemType.Shield:
            case ItemData.ItemType.ElectricityBall:
                itemDesc.text = string.Format(itemData.itemDesc, itemData.damages[level], itemData.counts[level]);
                break;
            case ItemData.ItemType.Power:
            case ItemData.ItemType.Cooldown:
            case ItemData.ItemType.Speed:
                itemDesc.text = string.Format(itemData.itemDesc, itemData.damages[level] * 100);
                break;
            case ItemData.ItemType.Heal:
                itemDesc.text = string.Format(itemData.itemDesc, itemData.damages[level]);
                break;
            default:
                itemDesc.text = string.Format(itemData.itemDesc);
                break;
        }
    }

    //https://youtu.be/-47pjK-P888?si=xD4m2bCoqxpnx-Vn&t=3526
    //무기 추가될 때 브로드케스트메시지 해주기
    public void LevelUp()
    {
        switch(itemData.itemType)
        {
            case ItemData.ItemType.Basic:
                float basicNextDamage = itemData.baseDamage;
                basicNextDamage = itemData.damages[level];
                int basicNextCount = itemData.counts[level];
                basicWeapon.LevelUp((int)basicNextDamage, basicNextCount);
                break;
            case ItemData.ItemType.Bomb:
                if(level == 0)
                {
                    boomWeapon.unlock = true;
                    boomWeapon.StartCoroutine("Boom");
                }
                else
                {
                    float nextDamage = itemData.baseDamage;

                    nextDamage += itemData.baseDamage * itemData.damages[level];

                    boomWeapon.LevelUp(nextDamage);
                }
                break;
            case ItemData.ItemType.ElectricityBall:
                if(level == 0)
                {
                    electricityBallWeapon.unlock = true;
                    electricityBallWeapon.Attack();
                }
                else
                {
                    float nextDamage = itemData.baseDamage;

                    nextDamage = itemData.baseDamage + itemData.damages[level];

                    electricityBallWeapon.LevelUp(nextDamage);
                }
                break;
            case ItemData.ItemType.Regeneration:
                break;
            case ItemData.ItemType.Power:
                break;
            case ItemData.ItemType.Cooldown:
            case ItemData.ItemType.Speed:
                if(level == 0)
                {
                    GameObject newAbility = new GameObject("AbilityObject");
                    abilityIncrease = newAbility.AddComponent<ItemAbilityIncrease>();
                    abilityIncrease.Init(itemData);
                }
                else
                {
                    float nextRate = itemData.damages[level];
                    abilityIncrease.LevelUp(nextRate);
                }
                break;
            case ItemData.ItemType.Shield:
                if (level == 0)
                {
                    shield.unlock = true;
                    shield.ShieldInit();
                    shield.StartCoroutine("ShieldCooldown");
                }
                // 쿨타임 감소 추가하기
                break;
            case ItemData.ItemType.Heal:
                break;
        }

        level++;

        if(level == itemData.damages.Length)
        {
            GetComponent<Button>().interactable = false;        // 버튼 비활성화
        }
    }
}
