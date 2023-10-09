using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState { MoveToAppearPoint = 0, Phase01, Phase02, Phase03}

public class Boss : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private float bossApeearPoint = 2.5f;
    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement movement;
    private BossAttack bossAttack;
    private BossHp bossHp;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        bossAttack = GetComponent<BossAttack>();
        bossHp = GetComponent<BossHp>();
    }

    public void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        StopCoroutine(bossState.ToString());
        GameManager.instance.boss = false;
        GameManager.instance.enemySpawner.GameStart();
        Destroy(gameObject);
    }

    public void ChangeState(BossState newState)
    {
        // 이전에 재생중이던 상태 종료
        StopCoroutine(bossState.ToString());

        bossState = newState;

        StartCoroutine(bossState.ToString());
    }

    private IEnumerator MoveToAppearPoint()
    {
        movement.Move(Vector3.down);

        while(true)
        {
            if(transform.position.y <= bossApeearPoint)
            {
                movement.Move(Vector3.zero);

                ChangeState(BossState.Phase01);
            }

            yield return null;
        }
    }

    private IEnumerator Phase01()
    {
        bossAttack.StartFiring(AttackType.CircleFire);

        while(true)
        {
            // 보스 체력이 70% 이하가 되면
            if(bossHp.CurrentHp <= bossHp.MaxHp * 0.7f)
            {
                bossAttack.StopFiring(AttackType.CircleFire);
                ChangeState(BossState.Phase02);
            }
            yield return null;
        }
    }

    private IEnumerator Phase02()
    {
        // 플레이어 위치를 기준으로 단일 공격
        bossAttack.StartFiring(AttackType.SingleFireToCenterPosition);

        // 처음 이동 방향을 오른쪽으로 설정
        Vector3 direction = Vector3.right;
        movement.Move(direction);

        while(true)
        {
            if(transform.position.x <= stageData.LimitMin.x || transform.position.x >= stageData.LimitMax.x)
            {
                direction *= -1;
                movement.Move(direction);
            }

            if(bossHp.CurrentHp <= bossHp.MaxHp * 0.3f)
            {
                bossAttack.StopFiring(AttackType.SingleFireToCenterPosition);
                ChangeState(BossState.Phase03);
            }

            yield return null;
        }
    }

    private IEnumerator Phase03()
    {
        bossAttack.StartFiring(AttackType.CircleFire);
        bossAttack.StartFiring(AttackType.SingleFireToCenterPosition);

        Vector3 direction = Vector3.right;
        movement.Move(direction);

        while(true)
        {
            if (transform.position.x <= stageData.LimitMin.x || transform.position.x >= stageData.LimitMax.x)
            {
                direction *= -1;
                movement.Move(direction);
            }

            yield return null;
        }
    }
}
