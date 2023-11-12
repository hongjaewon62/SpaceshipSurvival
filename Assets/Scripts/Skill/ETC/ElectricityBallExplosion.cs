using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityBallExplosion : MonoBehaviour
{
    [SerializeField]
    private float damage;
    private CircleCollider2D exlposionCollider;

    private void Awake()
    {
        exlposionCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        damage = GameManager.instance.player.GetComponent<ElectricityBallWeapon>().damage;
    }

    private void OnEnable()
    {
        exlposionCollider.radius = 3.8f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>().gameObject.activeSelf)
            {
                if (collision.GetComponent<EnemyHp>().currentHp > 0)
                {
                    collision.GetComponent<EnemyHp>().TakeDamage(damage);
                }
            }
        }
        else if (collision.CompareTag("Boss"))
        {
            if (collision.GetComponent<BossHp>().CurrentHp > 0)
            {
                collision.GetComponent<BossHp>().TakeDamage(damage);
            }
        }
    }
}
