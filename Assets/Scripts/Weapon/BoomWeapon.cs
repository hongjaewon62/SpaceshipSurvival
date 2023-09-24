using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private AudioClip boomAudio;
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
        GameObject[] enemys1 = objectManager.GetPool("Enemy1");
        GameObject[] enemys2 = objectManager.GetPool("Enemy2");
        GameObject[] enemys3 = objectManager.GetPool("Enemy3");
        GameObject[] meteorites = objectManager.GetPool("Meteorite");
        GameObject[] projectiles = objectManager.GetPool("EnemyBullet1");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        // 모든 적 데미지
        for(int i = 0; i < enemys1.Length; ++i)
        {
            if(enemys1[i].activeSelf)
            {
                enemys1[i].GetComponent<EnemyHp>().TakeDamage(damage);
            }
        }
        for (int i = 0; i < enemys2.Length; ++i)
        {
            if (enemys2[i].activeSelf)
            {
                enemys2[i].GetComponent<EnemyHp>().TakeDamage(damage);
            }
        }
        for (int i = 0; i < enemys3.Length; ++i)
        {
            if (enemys3[i].activeSelf)
            {
                enemys3[i].GetComponent<EnemyHp>().TakeDamage(damage);
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

        audioSource.clip = boomAudio;
        audioSource.Play();

        //Destroy(gameObject);
    }

    public void LevelUp(float damage)
    {
        this.damage = damage;
    }
}
