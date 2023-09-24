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
    public Vector3 followPosition;
    public int followDelay;
    public Transform player;
    public Queue<Vector3> playerPosition;

    [SerializeField]
    private ObjectManager objectManager;

    private void Awake()
    {
        playerPosition = new Queue<Vector3>();
    }

    private void Start()
    {
        StartCoroutine(TryAttack());
    }

    private void Update()
    {
        Watch();
        Follow();
    }

    private void Follow()
    {
        transform.position = followPosition;
    }

    private void Watch()
    {
        //Input position
        playerPosition.Enqueue(player.position);

        // OutPut position
        if(playerPosition.Count > followDelay)
        {
            followPosition = playerPosition.Dequeue();
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
                bulletL.transform.position = transform.position + Vector3.left * 0.2f;

                GameObject bulletR = objectManager.MakeObject("AssistanceBullet1");
                bulletR.transform.position = transform.position + Vector3.right * 0.2f;
                break;
        }
    }
}
