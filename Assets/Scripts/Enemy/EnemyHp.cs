using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public float defaultHp = 3f;
    public float maxHp;
    public float currentHp;
    private Enemy enemy;
    private SpriteRenderer spriteRender;
    private bool firstEnable = true;

    private void Awake()
    {
        maxHp = defaultHp;
        currentHp = maxHp;
        enemy = GetComponent<Enemy>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        currentHp = maxHp;
        if (firstEnable)
        {
            firstEnable = false;
        }
        else
        {
            IncreaseHp(GameManager.instance.distanceNum);
        }
        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHp <= 0)
        {
            Debug.Log("Àû Ã³Ä¡");
            enemy.Die();
        }
    }

    private void IncreaseHp(float distance)
    {
        maxHp = defaultHp * (distance * 0.0001f + 1);
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRender.color = Color.red;

        yield return new WaitForSeconds(0.05f);

        spriteRender.color = Color.white;
    }

    public void EnemyDataInit(EnemyData data)
    {
        maxHp = data.health;
        currentHp = maxHp;
    }    
}
