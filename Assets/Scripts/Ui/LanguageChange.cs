using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageChange : MonoBehaviour
{
    public string textKey;
    public ItemData itemData;
    public string itemDesc;

    private void Start()
    {
        LanaguageTextChange();
        Localization.instance.LanguageDropdownChange += LanaguageTextChange;
    }

    private void OnDestroy()
    {
        Localization.instance.LanguageDropdownChange -= LanaguageTextChange;
    }

    private string Localize(string key)
    {
        int keyIndex = Localization.instance.langs[0].value.FindIndex(x => x.ToLower() == key.ToLower());
        return Localization.instance.langs[Localization.instance.currentLangIndex].value[keyIndex];
    }

    private void LanaguageTextChange()
    {
        string textDamage = Localize("Damage");
        string textCooldown = Localize("Cooldown");
        string textCounts = Localize("Counts");
        string textAmounts = Localize("Amounts");
        if (GetComponent<TextMeshProUGUI>() != null && itemData == null)
        {
            GetComponent<TextMeshProUGUI>().text = Localize(textKey);
        }
        else if(GetComponent<TextMeshProUGUI>() != null && itemData != null)
        {
            itemDesc = Localize(textKey);
            switch (itemData.itemType)
            {
                case ItemData.ItemType.Basic:
                    itemDesc += "\n" + textDamage.Replace("\r", "") + " : {0}" + "\n" + textCounts.Replace("\r", "") + " : {1}";
                    break;
                case ItemData.ItemType.Bomb:
                case ItemData.ItemType.ElectricityBall:
                    itemDesc += "\n" + textDamage.Replace("\r", "") + " : {0}";
                    break;
                    
                case ItemData.ItemType.Regeneration:
                    itemDesc += "\n" + textAmounts.Replace("\r", "") + " : {0}";
                    break;
                case ItemData.ItemType.Shield:
                    itemDesc += "\n" + textCooldown.Replace("\r", "") + " : {0}";
                    break;
                case ItemData.ItemType.Power:
                case ItemData.ItemType.Cooldown:
                case ItemData.ItemType.Speed:
                case ItemData.ItemType.MaxHealth:
                    itemDesc += "\n{0}%";
                    break;
                case ItemData.ItemType.Heal:
                    break;
                    break;
                default:
                    break;
            }
        }
    }
}
