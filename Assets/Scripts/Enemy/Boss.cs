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
    private bool dead = false;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        bossAttack = GetComponent<BossAttack>();
        bossHp = GetComponent<BossHp>();
    }

    private void OnEnable()
    {
        dead = false;
        transform.position = new Vector3(0, 10, 0);
    }

    public void Die()
    {
        if(dead)
        {
            return;
        }

        dead = true;
        GameManager.instance.GetExp(20);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        StopCoroutine(bossState.ToString());
        GameManager.instance.boss = false;
        GameManager.instance.enemySpawner.GameStart();
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public void ChangeState(BossState newState)
    {
        // ������ ������̴� ���� ����
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
        //bossAttack.StartFiring(AttackType.CircleFire);
        bossAttack.Phase01();

        while(true)
        {
            // ���� ü���� 70% ���ϰ� �Ǹ�
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
        // �÷��̾� ��ġ�� �������� ���� ����
        //bossAttack.StartFiring(AttackType.SingleFireToCenterPosition);

        bossAttack.Phase02();

        // ó�� �̵� ������ ���������� ����
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
        //bossAttack.StartFiring(AttackType.CircleFire);
        //bossAttack.StartFiring(AttackType.SingleFireToCenterPosition);

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
