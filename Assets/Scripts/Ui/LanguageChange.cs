using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageChange : MonoBehaviour
{
    public string textKey;
    public ItemData itemData;
    public string itemDesc;
    public TextMeshProUGUI text;

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
                    itemDesc += "\n" + textDamage + " : {0}" + "\n" + textCounts + " : {1}";
                    break;
                case ItemData.ItemType.Bomb:
                case ItemData.ItemType.ElectricityBall:
                    //itemDesc.text = string.Format(itemData.itemDesc, itemData.damages[level], itemData.counts[level]);
                    itemDesc += "\n" + textDamage + " : {0}" + "\n" + textCooldown + " : {1}";
                    //itemDesc += "\nDamage : {0} \nCooldown: {1}";
                    break;
                    
                case ItemData.ItemType.Regeneration:
                case ItemData.ItemType.Shield:
                    break;
                //case ItemData.ItemType.Power:
                //case ItemData.ItemType.Cooldown:
                //case ItemData.ItemType.Speed:
                //case ItemData.ItemType.MaxHealth:
                //    itemDesc.text = string.Format(itemData.itemDesc, itemData.damages[level] * 100);
                //    break;
                //case ItemData.ItemType.Heal:
                //    itemDesc.text = string.Format(itemData.itemDesc, itemData.damages[level]);
                //    break;
                //default:
                //    itemDesc.text = string.Format(itemData.itemDesc);
                //    break;
            }
        }
    }

    public void RemoveR()
    {
        text.text = text.text.Replace("\r", "");
    }
}
