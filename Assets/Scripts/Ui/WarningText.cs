using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarningText : MonoBehaviour
{
    private float fadeTime = 0.5f;
    private TextMeshProUGUI warningText;

    private void Awake()
    {
        warningText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine("FadeLoop");
    }

    private IEnumerator FadeLoop()
    {
        while (true)
        {
            yield return StartCoroutine(FadeEffect(1, 0));

            yield return StartCoroutine(FadeEffect(0, 1));
        }
    }

    private IEnumerator FadeEffect(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = warningText.color;
            color.a = Mathf.Lerp(start, end, percent);
            warningText.color = color;

            yield return null;
        }
    }
}
