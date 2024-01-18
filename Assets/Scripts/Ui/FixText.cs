using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FixText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void RemoveR()
    {
        text.text = text.text.Replace("\r", "");
    }
}
