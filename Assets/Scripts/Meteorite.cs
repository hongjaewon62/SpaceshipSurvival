using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField]
    private float damage = 10;
    [SerializeField]
    private GameObject explosionPrefab;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }
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

        SoundManager.instance.PlaySfx("Explosion1");

        //Destroy(gameObject);
        gameObject.SetActive(false);

        transform.rotation = Quaternion.Euler(0, 0, -90);
    }
}
