using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScroller : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float scrollRange = 12.2f;
    [SerializeField]
    private float moveSpeed = 3.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.down;

    private PlayerHP playerHp;

    private void Awake()
    {
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
    }

    private void Update()
    {
        if (!GameManager.instance.time)
        {
            return;
        }

        if (!playerHp.dead)
        {
            // 배경이 moveDirection 방향으로 moveSpeed의 속도로 이동
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            if (transform.position.y <= -scrollRange)
            {
                transform.position = target.position + Vector3.up * scrollRange;
            }
        }
    }
}
