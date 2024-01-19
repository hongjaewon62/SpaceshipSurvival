using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Lang
{
    public string lang, langLocalize;
    public List<string> value = new List<string>();
}

public class Localization : MonoBehaviour
{
    public static Localization instance;
    const string languageURL = "https://docs.google.com/spreadsheets/d/1sJhADDRp3eGRu_GRi4gT_lsR-Bkm0jgHKzClcWCQJNU/export?format=tsv";
    public event System.Action LanguageDropdownChange = () => { };
    public event System.Action LanguageChange = () => { };
    public List<Lang> langs;

    public int currentLangIndex;

    [SerializeField]
    private LanguageDropdown languageDropdown;

    private void Awake()
    {
        instance = this;
        InitLang();
    }

    private void InitLang()
    {
        //PlayerPrefs.DeleteKey("LangIndex");
        int langIndex = PlayerPrefs.GetInt("LangIndex", -1);
        int systemIndex = langs.FindIndex(x => x.lang.ToLower() == Application.systemLanguage.ToString().ToLower());
        int index;
        if(systemIndex == -1)
        {
            systemIndex = 0;
        }
        if(langIndex == -1)
        {
            index = systemIndex;
        }
        else
        {
            index = langIndex;
        }

        SetLangIndex(index);
        Debug.Log("index" + systemIndex);
    }

    public void SetLangIndex(int index)
    {
        currentLangIndex = index;
        PlayerPrefs.SetInt("LangIndex", currentLangIndex);
        LanguageChange();
        LanguageDropdownChange();
    }

    [ContextMenu("언어 불러오기")]
    private void GetLang()
    {
        StartCoroutine(GetLangCoroutine());
    }

    IEnumerator GetLangCoroutine()
    {
        UnityWebRequest www = UnityWebRequest.Get(languageURL);
        yield return www.SendWebRequest();
        SetLangList(www.downloadHandler.text);
        //Debug.Log(www.downloadHandler.text);
    }

    private void SetLangList(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;
        string[,] sentence = new string[rowSize, columnSize];

        for(int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            for(int j = 0; j < columnSize; j++)
            {
                sentence[i, j] = column[j];
            }
        }

        langs = new List<Lang>();
        for(int i = 0; i < columnSize; i++)
        {
            Lang lang = new Lang();
            lang.lang = sentence[0, i];
            lang.langLocalize = sentence[1, i];

            for(int j = 2; j < rowSize; j++)
            {
                lang.value.Add(sentence[j, i]);
            }
            langs.Add(lang);
        }
    }
}
