using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityBallProjectile : MonoBehaviour
{
    //private Transform target;
    [SerializeField]
    private GameObject ElectricityBallExplosionPrefab;
    private ObjectManager objectManager;
    //private Movement movement;
    //[SerializeField]
    //private ObjectManager objectManager;
    //private GameObject enemyBoss;
    //private void Awake()
    //{
    //    movement = GetComponent<Movement>();
    //    objectManager = GameManager.instance.objManager;
    //    enemyBoss = GameManager.instance.bossObject;
    //}

    //private void OnEnable()
    //{
    //    SearchEnemy();
    //}

    private void Start()
    {
        objectManager = GameManager.instance.objManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>().gameObject.activeSelf)
            {
                if (collision.GetComponent<EnemyHp>().currentHp > 0)
                {
                    OnBoom();
                }
                gameObject.SetActive(false);
                transform.position = Vector3.zero;
            }
        }
        else if (collision.CompareTag("Boss"))
        {
            if (collision.GetComponent<BossHp>().CurrentHp > 0)
            {
                OnBoom();
            }
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }
    }

    //private void SearchEnemy()
    //{
    //    string[] enemyTypes = { "Enemy1", "Enemy2", "Enemy3", "Enemy4" };
    //    List<GameObject> activeEnemys = new List<GameObject>();
    //    List<GameObject> bossEnemys = new List<GameObject>();
    //    foreach (string enemyType in enemyTypes)
    //    {
    //        GameObject[] enemies = objectManager.GetPool(enemyType);
    //        foreach (GameObject enemy in enemies)
    //        {
    //            if (enemy.activeSelf)
    //            {
    //                activeEnemys.Add(enemy);
    //                Debug.Log(enemy);
    //            }
    //        }
    //    }

    //    for (int i = 0; i < enemyBoss.transform.childCount; i++)
    //    {
    //        if(enemyBoss.transform.GetChild(i).gameObject.activeSelf)
    //        {
    //            bossEnemys.Add(enemyBoss.transform.GetChild(i).gameObject);
    //        }
    //    }

    //    if (activeEnemys.Count > 0)
    //    {
    //        target = activeEnemys[Random.Range(0, activeEnemys.Count)].transform;
    //        Vector3 direction = (target.transform.position - transform.position).normalized;
    //        Debug.Log("Å¸°Ù" + target);
    //        movement.moveDirection = direction;
    //    }
    //    else if(bossEnemys.Count > 0)
    //    {
    //        target = bossEnemys[0].transform;
    //        Vector3 direction = (target.transform.position - transform.position).normalized;
    //        Debug.Log("Å¸°Ù" + target);
    //        movement.moveDirection = direction;
    //    }
    //}

    private void OnBoom()
    {
        //GameObject explosionPrefab = Instantiate(ElectricityBallExplosionPrefab, gameObject.transform.position, Quaternion.identity);
        //explosionPrefab.transform.position = transform.position;
        GameObject elploxion = objectManager.MakeObject("ElectricityBallExplosion");
        SoundManager.instance.PlaySfx("ElectricExplosion");
        elploxion.transform.position = transform.position;
    }
}
