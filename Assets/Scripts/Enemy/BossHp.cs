using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp : MonoBehaviour
{
    public float maxHp = 1000;
    [SerializeField]
    private GameObject bossHpPalen;
    private float currentHp;
    private Boss boss;

    public float MaxHp => maxHp;
    public float CurrentHp => currentHp;

    private void Awake()
    {
        currentHp = maxHp;
        boss = GetComponent<Boss>();
    }

    private void OnEnable()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        if(currentHp <= 0)
        {
            boss.Die();
            bossHpPalen.SetActive(false);
        }
    }
}
