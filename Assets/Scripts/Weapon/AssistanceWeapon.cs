using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistanceWeapon : MonoBehaviour
{
    public float attackCooldown = 1.0f;
    [SerializeField]
    private int maxAttackLevel = 2;
    [SerializeField]
    private int attackLevel = 1;
    [SerializeField]
    private int weaponLevel = 1;
    public Transform player;

    [SerializeField]
    private ObjectManager objectManager;

    private void OnEnable()
    {
        StartCoroutine(TryAttack());
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if(weaponLevel == 1)
        {
            transform.position = player.transform.position + new Vector3(0.8f, 0, 0);
        }
        else
        {
            transform.position = player.transform.position + new Vector3(-0.8f, 0, 0);
        }

    }

    private IEnumerator TryAttack()
    {
        while (true)
        {
            AttackByLevel();
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    private void AttackByLevel()
    {
        switch (attackLevel)
        {
            case 1:
                GameObject bullet = objectManager.MakeObject("AssistanceBullet1");
                bullet.transform.position = transform.position;
                break;
            case 2:

                GameObject bulletL = objectManager.MakeObject("AssistanceBullet1");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;

                GameObject bulletR = objectManager.MakeObject("AssistanceBullet1");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                break;
        }
    }
}
