using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAbilityIncrease : MonoBehaviour
{
    public ItemData.ItemType itemType;
    public float rate;

    public void Init(ItemData data)
    {
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        itemType = data.itemType;
        rate = data.damages[0];
        ApplyItem();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;

        ApplyItem();
    }

    private void ApplyItem()
    {
        switch (itemType)
        {
            case ItemData.ItemType.Cooldown:
                AttackSpeed();
                break;
            case ItemData.ItemType.Speed:
                PlayerSpeed();
                break;
        }
    }
    private void AttackSpeed()
    {
        BasicWeapon basicWeapon = transform.parent.GetComponentInChildren<BasicWeapon>();
        BoomWeapon boomWeapon = transform.parent.GetComponentInChildren<BoomWeapon>();

        basicWeapon.attackCooldown = 0.3f * (1f - rate);
        Debug.Log(rate + ", " + (1f - rate));
        boomWeapon.coolDown = 50f * (1f - rate);
    }

    private void PlayerSpeed()
    {
        float speed = 5f;
        GameManager.instance.player.transform.GetComponent<Movement>().moveSpeed = speed + speed * rate;
    }
}
