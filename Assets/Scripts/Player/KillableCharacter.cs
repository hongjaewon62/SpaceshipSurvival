using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BulletPro;
using UnityEngine.UI;

// This script is part of the BulletPro package for Unity.
// But it's only used in the example scene and I recommend writing a better one that fits your needs.
// Author : Simon Albou <albou.simon@gmail.com>

namespace BulletPro.DemoScripts
{
	public class KillableCharacter : MonoBehaviour
	{
		[Header("Stats")]
		public float maxHealth = 100;
		[System.NonSerialized] public float curHealth;

		[Header("References")]
		public BulletEmitter[] bulletEmitters;
		public SpriteRenderer sprite;
		public BulletReceiver receiver;
		public Transform player;
		public Transform lifebar;
		[System.NonSerialized] public SpriteRenderer lifebarSprite;
		[System.NonSerialized] public Slider lifebarSlider;
		private PlayerHP playerHp;
		private PlayerHealthBar playerHealthBar;
		private PlayerController playerController;

		[System.NonSerialized] public bool isAlive;
		Coroutine fadeAlpha;

		[Header("Events")]
		public UnityEvent onHurt;
		public UnityEvent onDeath;
		public UnityEvent onRespawn;

		void Awake()
		{
			lifebarSprite = lifebar.GetComponent<SpriteRenderer>();
			lifebarSlider = lifebar.GetComponent<Slider>();
			playerHealthBar = lifebarSlider.GetComponent<PlayerHealthBar>();
			playerHp = player.GetComponent<PlayerHP>();
			maxHealth = player.GetComponent<PlayerHP>().maxHealth;
			playerController = player.GetComponent<PlayerController>();
		}

        // 충돌시 플레이어 체력 감소
        public void Hurt(Bullet bullet, Vector3 hitPoint)
		{
			if (playerHp.dead)
			{
				return;
			}
			//playerHp.currentHealth -= bullet.moduleParameters.GetFloat("_PowerLevel");
			playerHp.currentHealth -= 10;
			playerHealthBar.SetHealth(playerHp.currentHealth);
			if (playerHp.currentHealth > 0)
			{
				if (onHurt != null)
					onHurt.Invoke();
			}
			else
			{
				//Die();
				playerController.DIe();
			}
		}

		void Die()
		{
			isAlive = false;
			if (lifebarSprite != null)
			{
				lifebarSprite.enabled = false;
			}

			for (int i = 0; i < bulletEmitters.Length; i++)
			{
				bulletEmitters[i].Kill();
			}

			receiver.enabled = false;

			if (onDeath != null)
            {
				onDeath.Invoke();
			}
		}

		public void AlphaFadeOut() { fadeAlpha = StartCoroutine(FadeAlpha(1.5f)); }

		IEnumerator FadeAlpha(float duration)
		{
			float innerTimer = 0;
			while (innerTimer < duration)
			{
				innerTimer += Time.deltaTime;
				Color cur = Color.white;
				cur.a = 1 - innerTimer / duration;
				sprite.color = cur;
				yield return null;
			}
			sprite.enabled = false;
		}

		//public void Respawn()
		//{
		//	curHealth = maxHealth;
		//	UpdateLifebar();
		//	isAlive = true;
		//	lifebarSprite.enabled = true;
		//	if (fadeAlpha != null) StopCoroutine(fadeAlpha);
		//	sprite.enabled = true;
		//	sprite.color = Color.white;
		//	receiver.enabled = true;
		//	if (onRespawn != null) onRespawn.Invoke();

		//	for (int i = 0; i < bulletEmitters.Length; i++)
		//	{
		//		bulletEmitters[i].Kill();
		//		if (bulletEmitters[i].playAtStart)
		//			bulletEmitters[i].Boot();
		//	}
		//}

		public void UpdateLifebar()
		{
			lifebarSlider.value = curHealth;
		}
	}
}