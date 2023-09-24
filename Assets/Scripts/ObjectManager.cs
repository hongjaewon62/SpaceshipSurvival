using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public GameObject meteoritePrefab;
    public GameObject warningLinePrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemRegenerationPrefab;
    public GameObject playerBullet1Prefab;
    public GameObject assistanceBulletPrefab;
    public GameObject enemyBullet1Prefab;
    public GameObject enemyBullet2Prefab;

    private GameObject[] enemy1;
    private GameObject[] enemy2;
    private GameObject[] enemy3;

    private GameObject[] meteorite;
    private GameObject[] warningLine;

    private GameObject[] itemCoin;
    private GameObject[] itemRegeneration;

    private GameObject[] playerBullet1;
    private GameObject[] assistanceBullet1;

    private GameObject[] enemyBullet1;
    private GameObject[] enemyBullet2;

    private GameObject[] targetPool;

    private void Awake()
    {
        enemy1 = new GameObject[10];
        enemy2 = new GameObject[20];
        enemy3 = new GameObject[10];

        meteorite = new GameObject[10];
        warningLine = new GameObject[10];

        itemCoin = new GameObject[20];
        itemRegeneration = new GameObject[20];

        playerBullet1 = new GameObject[200];
        assistanceBullet1 = new GameObject[20];

        enemyBullet1 = new GameObject[200];
        enemyBullet2 = new GameObject[30];

        Generate();
    }

    private void Generate()
    {
        // enemy
        for(int i = 0; i < enemy1.Length; i++)
        {
            enemy1[i] = Instantiate(enemy1Prefab);
            enemy1[i].SetActive(false);
        }
        for(int i = 0; i < enemy2.Length; i++)
        {
            enemy2[i] = Instantiate(enemy2Prefab);
            enemy2[i].SetActive(false);
        }
        for (int i = 0; i < enemy3.Length; i++)
        {
            enemy3[i] = Instantiate(enemy3Prefab);
            enemy3[i].SetActive(false);
        }

        // meteorite
        for (int i = 0; i < meteorite.Length; i++)
        {
            meteorite[i] = Instantiate(meteoritePrefab);
            meteorite[i].SetActive(false);
        }
        for (int i = 0; i < warningLine.Length; i++)
        {
            warningLine[i] = Instantiate(warningLinePrefab);
            warningLine[i].SetActive(false);
        }

        // item
        for (int i = 0; i < itemCoin.Length; i++)
        {
            itemCoin[i] = Instantiate(itemCoinPrefab);
            itemCoin[i].SetActive(false);
        }
        for (int i = 0; i < itemRegeneration.Length; i++)
        {
            itemRegeneration[i] = Instantiate(itemRegenerationPrefab);
            itemRegeneration[i].SetActive(false);
        }

        // bullet
        for (int i = 0; i < playerBullet1.Length; i++)
        {
            playerBullet1[i] = Instantiate(playerBullet1Prefab);
            playerBullet1[i].SetActive(false);
        }
        for (int i = 0; i < assistanceBullet1.Length; i++)
        {
            assistanceBullet1[i] = Instantiate(assistanceBulletPrefab);
            assistanceBullet1[i].SetActive(false);
        }
        for (int i = 0; i < enemyBullet1.Length; i++)
        {
            enemyBullet1[i] = Instantiate(enemyBullet1Prefab);
            enemyBullet1[i].SetActive(false);
        }
        for (int i = 0; i < enemyBullet2.Length; i++)
        {
            enemyBullet2[i] = Instantiate(enemyBullet2Prefab);
            enemyBullet2[i].SetActive(false);
        }
    }

    public GameObject MakeObject(string type)
    {
        switch(type)
        {
            case "Enemy1":
                targetPool = enemy1;
                break;
            case "Enemy2":
                targetPool = enemy2;
                break;
            case "Enemy3":
                targetPool = enemy3;
                break;
            case "Meteorite":
                targetPool = meteorite;
                break;
            case "WarningLine":
                targetPool = warningLine;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemRegeneration":
                targetPool = itemRegeneration;
                break;
            case "PlayerBullet1":
                targetPool = playerBullet1;
                break;
            case "AssistanceBullet1":
                targetPool = assistanceBullet1;
                break;
            case "EnemyBullet1":
                targetPool = enemyBullet1;
                break;
            case "EnemyBullet2":
                targetPool = enemyBullet2;
                break;
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "Enemy1":
                targetPool = enemy1;
                break;
            case "Enemy2":
                targetPool = enemy2;
                break;
            case "Enemy3":
                targetPool = enemy3;
                break;
            case "Meteorite":
                targetPool = meteorite;
                break;
            case "WarningLine":
                targetPool = warningLine;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemRegeneration":
                targetPool = itemRegeneration;
                break;
            case "PlayerBullet1":
                targetPool = playerBullet1;
                break;
            case "AssistanceBullet1":
                targetPool = assistanceBullet1;
                break;
            case "EnemyBullet1":
                targetPool = enemyBullet1;
                break;
            case "EnemyBullet2":
                targetPool = enemyBullet2;
                break;
        }

        return targetPool;
    }
}
