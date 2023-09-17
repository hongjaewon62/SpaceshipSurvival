using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 3f;
    public float currentHp;
    private Enemy enemy;
    private SpriteRenderer spriteRender;

    private void Awake()
    {
        currentHp = maxHp;
        enemy = GetComponent<Enemy>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        currentHp = maxHp;
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
