using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private float damage = 50;
    public float coolDown = 50;
    public bool unlock = false;
    [SerializeField]
    private ObjectManager objectManager;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //StartCoroutine("Boom");
    }

    public IEnumerator Boom()
    {
        if(unlock)
        {
            while (true)
            {
                OnBoom();
                yield return new WaitForSeconds(coolDown);
            }
        }
    }

    public void OnBoom()
    {
        Vector3 startPosition = Vector3.zero;
        Instantiate(explosionPrefab, startPosition, Quaternion.identity);
        //GameObject[] enemys1 = objectManager.GetPool("Enemy1");
        //GameObject[] enemys2 = objectManager.GetPool("Enemy2");
        //GameObject[] enemys3 = objectManager.GetPool("Enemy3");
        //GameObject[] enemys4 = objectManager.GetPool("Enemy4");
        GameObject[] meteorites = objectManager.GetPool("Meteorite");
        GameObject[] projectiles = objectManager.GetPool("EnemyBullet1");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        string[] enemyTypes = { "Enemy1", "Enemy2", "Enemy3", "Enemy4" };

        foreach (string enemyType in enemyTypes)
        {
            GameObject[] enemies = objectManager.GetPool(enemyType);

            foreach (GameObject enemy in enemies)
            {
                if (enemy.activeSelf)
                {
                    enemy.GetComponent<EnemyHp>().TakeDamage(damage);
                }
            }
        }

        // 모든 운석 파괴
        for (int i = 0; i < meteorites.Length; ++i)
        {
            if(meteorites[i].activeSelf)
            {
                meteorites[i].GetComponent<Meteorite>().Die();
            }
        }

        for(int i = 0; i< projectiles.Length; ++i)
        {
            if(projectiles[i].activeSelf)
            {
                projectiles[i].GetComponent<EnemyProjectile>().Die();
            }
        }

        if(boss != null)
        {
            boss.GetComponent<BossHp>().TakeDamage(damage);
        }

        audioSource.clip = clip;
        audioSource.Play();

        //Destroy(gameObject);
    }

    public void LevelUp(float damage)
    {
        this.damage = damage;
    }
}
