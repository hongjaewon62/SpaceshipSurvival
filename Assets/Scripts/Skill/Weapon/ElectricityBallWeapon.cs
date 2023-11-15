using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityBallWeapon : MonoBehaviour
{
    public float attackCooldown = 10f;
    public float damage = 5f;
    public bool unlock = false;
    private Transform target;
    [SerializeField]
    private GameObject ElectricityBallPrefab;
    [SerializeField]
    private GameObject enemyBoss;
    [SerializeField]
    private ObjectManager objectManager;

    //private IEnumerator Boom()
    //{
    //    if (unlock)
    //    {
    //        while (true)
    //        {
    //            OnBoom();
    //            yield return new WaitForSeconds(coolDown);
    //        }
    //    }
    //}
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }
    public void Attack()
    {
        StartCoroutine(TryAttack());
    }

    private IEnumerator TryAttack()
    {
        if(unlock)
        {
            while (true)
            {

                //Instantiate(ElectricityBallPrefab, GameManager.instance.player.transform.position, Quaternion.identity);
                GameObject bullet = objectManager.MakeObject("ElectricityBall");
                bullet.transform.position = transform.position;
                SearchEnemy(bullet);
                yield return new WaitForSeconds(attackCooldown);
            }
        }
    }

    private void SearchEnemy(GameObject bullet)
    {
        string[] enemyTypes = { "Enemy1", "Enemy2", "Enemy3", "Enemy4", "Enemy5", "Enemy6" };
        List<GameObject> activeEnemys = new List<GameObject>();
        List<GameObject> bossEnemys = new List<GameObject>();
        foreach (string enemyType in enemyTypes)
        {
            GameObject[] enemies = objectManager.GetPool(enemyType);
            foreach (GameObject enemy in enemies)
            {
                if (enemy.activeSelf)
                {
                    activeEnemys.Add(enemy);
                    Debug.Log(enemy);
                }
            }
        }

        for (int i = 0; i < enemyBoss.transform.childCount; i++)
        {
            if (enemyBoss.transform.GetChild(i).gameObject.activeSelf)
            {
                bossEnemys.Add(enemyBoss.transform.GetChild(i).gameObject);
            }
        }

        if (activeEnemys.Count > 0)
        {
            target = activeEnemys[Random.Range(0, activeEnemys.Count)].transform;
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Debug.Log("Å¸°Ù" + target);
            bullet.GetComponent<Movement>().moveDirection = direction;
        }
        else if (bossEnemys.Count > 0)
        {
            target = bossEnemys[0].transform;
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Debug.Log("Å¸°Ù" + target);
            bullet.GetComponent<Movement>().moveDirection = direction;
        }
    }

    public void LevelUp(float damage)
    {
        this.damage = damage;
    }
}
