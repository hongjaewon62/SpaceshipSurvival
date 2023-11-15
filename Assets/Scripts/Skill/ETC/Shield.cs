using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float cooldown;
    public bool shield = false;
    public bool unlock = false;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameManager.instance.player.GetComponent<PlayerController>();
        gameObject.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        gameObject.transform.position = GameManager.instance.player.transform.position;
    }

    public IEnumerator ShieldCooldown()
    {
        if (unlock)
        {
            while (true)
            {
                if (!shield)
                {
                    yield return new WaitForSeconds(cooldown);
                    ShieldInit();
                }

                yield return null;
            }
        }
    }

    public void ShieldInit()
    {
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        playerController.defence = 10000;
        shield = true;
    }

    public void ShieldHit()
    {
        if(shield)
        {
            shield = false;
            playerController.defence = 0;
            gameObject.transform.localScale = Vector3.zero;
        }
    }

    public void LevelUp(float cooldown)
    {
        this.cooldown = cooldown;
        GameManager.instance.player.BroadcastMessage("ApplyItem", SendMessageOptions.DontRequireReceiver);
    }
}
