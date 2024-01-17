using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageChange : MonoBehaviour
{
    public string textKey;

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
        if(GetComponent<TextMeshProUGUI>() != null)
        {
            GetComponent<TextMeshProUGUI>().text = Localize(textKey);
        }
    }
}
