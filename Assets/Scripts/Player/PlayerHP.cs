using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float maxHealth = 50;
    public float currentHealth;
    public float recoveryHealthAmount = 30;
    private float regenerationAmount = 0.5f;
    private float regenerationCooldown = 2f;

    [SerializeField]
    private PlayerHealthBar healthBar;
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Shield shield;
    public bool unlock = false;

    public bool dead = false;

    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    public void Init()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if ((damage - playerController.defence) > 0 || shield.shield == false)
        {
            currentHealth -= (damage - playerController.defence);
            StopCoroutine("HitColorAnimation");
            StartCoroutine("HitColorAnimation");
        }
        else if(shield.shield == true)
        {
            shield.ShieldHit();
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

    public void RegenerationStart()
    {
        StartCoroutine(PlayerRegenerationHp());
    }

    private IEnumerator PlayerRegenerationHp()
    {
        if (unlock)
        {
            while (true)
            {
                currentHealth += regenerationAmount;
                if (currentHealth >= maxHealth)
                {
                    currentHealth = maxHealth;
                }

                healthBar.SetHealth(currentHealth);
                yield return new WaitForSeconds(regenerationCooldown);
            }
        }
    }

    public void LevelUp(float amount, float cooldown)
    {
        regenerationAmount = amount;
        regenerationCooldown = cooldown;
    }
}
