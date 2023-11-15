using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    [SerializeField]
    private BossHp[] bossHp;
    private Slider sliderHp;
    private EnemySpawner enemySpawner;
    private float learpSpeed;

    private void Awake()
    {
        sliderHp = GetComponent<Slider>();
    }

    private void Start()
    {
        enemySpawner = GameManager.instance.enemySpawner.GetComponent<EnemySpawner>();
    }

    private void Update()
    {
        learpSpeed = 5f * Time.deltaTime;

        HealthBarFiller();
    }

    private void HealthBarFiller()
    {
        //sliderHp.value = bossHp[0].CurrentHp / bossHp[0].MaxHp;
        Debug.Log(enemySpawner.randomBossIndex);
        sliderHp.value = Mathf.Lerp(sliderHp.value, bossHp[enemySpawner.randomBossIndex].CurrentHp / bossHp[enemySpawner.randomBossIndex].MaxHp, learpSpeed);
    }
}
