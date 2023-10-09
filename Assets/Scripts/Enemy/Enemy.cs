using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string enemyName;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int scorePoint = 100;       // 적 처치시 획득 점수
    [SerializeField]
    private float maxShotDelay;
    [SerializeField]
    private float currentShotDelay;
    [SerializeField]
    private GameObject explosionPrefab;
    //[SerializeField]
    //private GameObject[] itemPrefabs;       // 드랍 아이템
    [SerializeField]
    private string[] itemPrefab;
    [SerializeField]
    private int[] itemDropChance;
    public GameObject player;
    private PlayerController playerController;
    private ObjectManager objectManager;
    //private bool isDead = false;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        objectManager = GameObject.FindGameObjectWithTag("ObjectManager").GetComponent<ObjectManager>();
    }

    private void Update()
    {
        if (!GameManager.instance.time)
        {
            return;
        }

        Fire();
        Reload();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(gameObject.activeSelf)
            {
                collision.GetComponent<PlayerHP>().TakeDamage(damage);
                Die();
            }
        }
    }

    public void Die()
    {

        //playerController.Score += scorePoint;
        GameManager.instance.kill++;
        GameManager.instance.GetExp();
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        SpawnItem();

        //isDead = true;

        //Destroy(gameObject);
        gameObject.SetActive(false);
        transform.rotation = Quaternion.identity;
        if(enemyName == "4")
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    private void Fire()
    {
        if(currentShotDelay < maxShotDelay)
        {
            return;
        }

        if (enemyName == "1")
        {
            return;
        }
        else if (enemyName == "2")
        {
            return;
        }
        else if(enemyName == "3" || enemyName == "4")
        {
            GameObject projectile = objectManager.MakeObject("EnemyBullet2");
            projectile.transform.position = transform.position;

            Vector3 direction = (player.transform.position - transform.position).normalized;
            projectile.GetComponent<Movement>().Move(direction);
        }

        currentShotDelay = 0;
    }

    private void Reload()
    {
        currentShotDelay += Time.deltaTime;
    }

    private void SpawnItem()
    {
        int randomNumber = Random.Range(0, 100);

        for(int i = 0; i < itemPrefab.Length; i++)
        {
            if (randomNumber < itemDropChance[i])
            {
                GameObject item = objectManager.MakeObject(itemPrefab[i]);
                item.transform.position = transform.position;
                //Instantiate(itemPrefabs[i], transform.position, Quaternion.identity);
            }
        }
    }

    public void EnemyDataInit(EnemyData data)
    {
        damage = data.damage;
    }
}
