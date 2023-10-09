using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private void OnEnable()
    {
        gameObject.GetComponent<Movement>().Move(Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            if(collision.GetComponent<Enemy>().gameObject.activeSelf)
            {
                if (collision.GetComponent<EnemyHp>().currentHp > 0)
                {
                    collision.GetComponent<EnemyHp>().TakeDamage(damage);
                }
                // ÃÑ¾Ë »èÁ¦
                //Destroy(gameObject);
                gameObject.SetActive(false);
                transform.position = Vector3.zero;
            }
        }
        else if(collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossHp>().TakeDamage(damage);

            //Destroy(gameObject);
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }
    }

    public void IncreaseDamage(int damage)
    {
        this.damage = damage;
    }
}
