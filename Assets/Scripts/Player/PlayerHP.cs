using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100;
    public float currentHealth;
    public float recoveryHealthAmount = 1;

    [SerializeField]
    private PlayerHealthBar healthBar;
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;

    public bool dead = false;

    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(int damage)
    {
        if ((damage - playerController.defence) > 0)
        {
            currentHealth -= (damage - playerController.defence);
            StopCoroutine("HitColorAnimation");
            StartCoroutine("HitColorAnimation");
        }
        else
        {
            currentHealth -= 1;
        }

        if (currentHealth > 0)
        {
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            if (!dead)
            {
                healthBar.SetHealth(currentHealth);
                Debug.Log("Die");

                dead = true;
                playerController.DIe();
            }
        }
    }

    public void PlayerRecoveryHp(float amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
    }

    public void PlayerIncreaseHp(int amount)
    {
        maxHealth += amount;
        healthBar.SetHealth(currentHealth);
    }

    public void PlayerDecreaseHp(int amount)
    {
        maxHealth -= amount;
        healthBar.SetHealth(currentHealth);
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }
}
