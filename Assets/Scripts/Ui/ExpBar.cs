using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpBar : MonoBehaviour
{
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI levelText;
    private float learpSpeed;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        learpSpeed = 7f * Time.deltaTime;
    }


    private void LateUpdate()
    {
        ExperienceSystems();

        levelText.text = string.Format("LV.{0:f0}", GameManager.instance.level);
    }

    private void ExperienceSystems()
    {
        float currentExp = GameManager.instance.exp;
        float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
        slider.value = Mathf.Lerp(slider.value, currentExp / maxExp, learpSpeed);
    }
}
