using BulletPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { CircleFire = 0, SingleFireToCenterPosition}
public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    private ObjectManager objectManager;
    private BulletEmitter bulletEmitter;
    public EmitterProfile[] emitterProfile;

    private void Awake()
    {
        objectManager = GameObject.FindGameObjectWithTag("ObjectManager").GetComponent<ObjectManager>();
        bulletEmitter = GetComponent<BulletEmitter>();
    }

    public void Phase01()
    {
        bulletEmitter.SwitchProfile(emitterProfile[0]);
    }

    public void Phase02()
    {
        bulletEmitter.SwitchProfile(emitterProfile[1]);
    }

    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }

    private IEnumerator CircleFire()
    {
        float attackRate = 0.5f;                // 공격 주기
        int count = 30;                         // 발사체 생성 개수
        float intervalAngle = 360 / count;      // 발사체 사이의 각도
        float weightAngle = 0;                  // 가중되는 각도(같은 위치로 발사하지 않도록 설정)
        
        // 원 형태로 발사체 생성(count 개수만큼)
        while(true)
        {
            for(int i = 0; i < count; ++ i)
            {
                GameObject projectile = objectManager.MakeObject("EnemyBullet1");
                if(projectile == null)              // 널 버그가 생기는 경우가 있어서 추가
                {
                    StopCoroutine("CircleFire");
                    yield return null;
                }
                projectile.transform.position = transform.position;

                float angle = weightAngle + intervalAngle * i;

                float x = Mathf.Cos(angle * Mathf.PI / 180.0f); // Cos(각도), 라디안 단위의 각도 표현을 위해 PI / 180
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

                projectile.GetComponent<Movement>().Move(new Vector2(x, y));
            }

            weightAngle += 1;

            yield return new WaitForSeconds(attackRate);
        }
    }

    private IEnumerator SingleFireToCenterPosition()
    {
        Vector3 targetPosition = Vector3.zero;
        float attackRate = 0.1f;

        while(true)
        {
            GameObject projectile = objectManager.MakeObject("EnemyBullet1");
            if (projectile == null)              // 널 버그가 생기는 경우가 있어서 추가
            {
                StopCoroutine("SingleFireToCenterPosition");
                yield return null;
            }
            projectile.transform.position = transform.position;
            // 발사체 이동방향
            Vector3 direction = (targetPosition - projectile.transform.position).normalized;
            projectile.GetComponent<Movement>().Move(direction);

            yield return new WaitForSeconds(attackRate);

        }
    }
}
