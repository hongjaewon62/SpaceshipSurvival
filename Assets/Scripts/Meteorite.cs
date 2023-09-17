using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private GameObject explosionPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);

            Die();
        }
    }

    public void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
