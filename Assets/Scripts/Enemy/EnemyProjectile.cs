using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float damage = 1;
    private void OnEnable()
    {
        gameObject.GetComponent<Movement>().Move(Vector3.up);
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void Die()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
