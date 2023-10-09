using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Vector3 moveDirection = Vector3.zero;

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
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    public void Move(Vector3 direction)
    {
        moveDirection = direction;
    }
}
