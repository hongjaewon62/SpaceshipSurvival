using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { PowerUp = 0, Health}
public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemType itemType;
    private Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();

        float x = Random.Range(-1.0f, 1.0f);
        float y = Random.Range(-1.0f, 1.0f);

        movement.Move(new Vector3(x, y, 0));
    }

    private void OnEnable()
    {
        float x = Random.Range(-1.0f, 1.0f);
        float y = Random.Range(-1.0f, 1.0f);

        movement.Move(new Vector3(x, y, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Use(collision.gameObject);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void Use(GameObject player)
    {
        switch(itemType)
        {
            case ItemType.PowerUp:
                player.GetComponent<BasicWeapon>().AttackLevel++;
                break;
            case ItemType.Health:
                player.GetComponent<PlayerHP>().PlayerRecoveryHp(player.GetComponent<PlayerHP>().recoveryHealthAmount);
                //hp bar 가 업데이트가 안됨
                break;
        }
    }
}
