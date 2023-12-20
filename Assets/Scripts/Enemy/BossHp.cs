using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp : MonoBehaviour
{
    public float defaultHp = 500f;
    public float maxHp = 500f;
    [SerializeField]
    private GameObject bossHpPalen;
    private float currentHp;
    private Boss boss;

    public float MaxHp => maxHp;
    public float CurrentHp => currentHp;

    private void Awake()
    {
        maxHp = defaultHp;
        currentHp = maxHp;
        boss = GetComponent<Boss>();
    }

    private void OnEnable()
    {
        IncreaseBossHp(GameManager.instance.bossCount);
        currentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        if(currentHp <= 0)
        {
            boss.Die();
            GameManager.instance.bossCount++;
            bossHpPalen.SetActive(false);
        }
    }

    private void IncreaseBossHp(int bossCount)
    {
        maxHp = defaultHp * bossCount;
    }
}
