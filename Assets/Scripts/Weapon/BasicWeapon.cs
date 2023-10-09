using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public float attackCooldown = 0.3f;
    [SerializeField]
    private int maxAttackLevel = 5;
    [SerializeField]
    private int attackLevel = 1;
    [SerializeField]
    private AudioClip clip;
    private AudioSource audioSource;

    [SerializeField]
    private ObjectManager objectManager;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public int AttackLevel
    {
        set => attackLevel = Mathf.Clamp(value, 1, maxAttackLevel);
        get => attackLevel;
    }

    public void StartFireing()
    {
        StartCoroutine("TryAttack");
    }

    public void StopFireing()
    {
        StopCoroutine("TryAttack");
    }

    private IEnumerator TryAttack()
    {
        while(true)
        {
            //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            AttackByLevel();
            audioSource.clip = clip;
            audioSource.Play();

            yield return new WaitForSeconds(attackCooldown);
        }
    }

    private void AttackByLevel()
    {
        GameObject differentDirectionBullet = null;

        switch(attackLevel)
        {
            case 1:
                //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                GameObject bullet =  objectManager.MakeObject("PlayerBullet1");
                bullet.transform.position = transform.position;
                break;
            //case 2:
            //    //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            //    bullet = objectManager.MakeObject("PlayerBullet1");
            //    //bullet.transform.GetComponent<Projectile>().IncreaseDamage(3);
            //    bullet.transform.position = transform.position;
            //    break;
            case 2:
                //Instantiate(bulletPrefab, transform.position + Vector3.left * 0.2f, Quaternion.identity);
                //Instantiate(bulletPrefab, transform.position + Vector3.right * 0.2f, Quaternion.identity);

                GameObject bulletL = objectManager.MakeObject("PlayerBullet1");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;

                GameObject bulletR = objectManager.MakeObject("PlayerBullet1");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                break;
            case 3:
                bullet = objectManager.MakeObject("PlayerBullet1");
                bullet.transform.position = transform.position;
                bulletL = objectManager.MakeObject("PlayerBullet1");
                bulletL.transform.position = transform.position + Vector3.left * 0.15f;

                bulletR = objectManager.MakeObject("PlayerBullet1");
                bulletR.transform.position = transform.position + Vector3.right * 0.15f;
                break;
            case 4:
                GameObject bulletInL = objectManager.MakeObject("PlayerBullet1");
                bulletInL.transform.position = transform.position + Vector3.left * 0.0725f;
                GameObject bulletInR = objectManager.MakeObject("PlayerBullet1");
                bulletInR.transform.position = transform.position + Vector3.right * 0.0725f;
                bulletL = objectManager.MakeObject("PlayerBullet1");
                bulletL.transform.position = transform.position + Vector3.left * 0.2f;

                bulletR = objectManager.MakeObject("PlayerBullet1");
                bulletR.transform.position = transform.position + Vector3.right * 0.2f;
                break;
            case 5:
                bullet = objectManager.MakeObject("PlayerBullet1");
                bullet.transform.position = transform.position;
                bulletL = objectManager.MakeObject("PlayerBullet1");
                bulletL.transform.position = transform.position + Vector3.left * 0.15f;

                bulletR = objectManager.MakeObject("PlayerBullet1");
                bulletR.transform.position = transform.position + Vector3.right * 0.15f;

                //differentDirectionBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                //differentDirectionBullet.GetComponent<Movement>().Move(new Vector3(-0.2f, 1, 0));

                differentDirectionBullet = objectManager.MakeObject("PlayerBullet1");
                differentDirectionBullet.transform.position = transform.position;
                differentDirectionBullet.GetComponent<Movement>().Move(new Vector3(-0.225f, 1, 0));

                //differentDirectionBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                //differentDirectionBullet.GetComponent<Movement>().Move(new Vector3(0.2f, 1, 0));

                differentDirectionBullet = objectManager.MakeObject("PlayerBullet1");
                differentDirectionBullet.transform.position = transform.position;
                differentDirectionBullet.GetComponent<Movement>().Move(new Vector3(0.225f, 1, 0));
                break;
        }
    }

    public void LevelUp(int damage, int level)
    {
        GameObject[] bullet = objectManager.GetPool("PlayerBullet1");
        foreach (GameObject playerBullet in bullet)
        {
            playerBullet.transform.GetComponent<Projectile>().IncreaseDamage(damage);
        }
        attackLevel = level;
    }
}
