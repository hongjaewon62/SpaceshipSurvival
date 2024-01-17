using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageDropdown : MonoBehaviour
{
    private TMP_Dropdown dropdown;


    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        if(dropdown.options.Count != Localization.instance.langs.Count)
        {
            SetLangOption();
        }
        dropdown.onValueChanged.AddListener((d) => Localization.instance.SetLangIndex(dropdown.value));
        LanguageDropdownChange();
        Localization.instance.LanguageDropdownChange += LanguageDropdownChange;
    }

    private void OnDestroy()
    {
        Localization.instance.LanguageDropdownChange -= LanguageDropdownChange;
    }

    private void SetLangOption()
    {
        List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i < Localization.instance.langs.Count; i++)
        {
            optionDatas.Add(new TMP_Dropdown.OptionData() { text = Localization.instance.langs[i].langLocalize });
        }
        dropdown.options = optionDatas;
    }

    public void LanguageDropdownChange()
    {
        dropdown.value = Localization.instance.currentLangIndex;
    }
}
