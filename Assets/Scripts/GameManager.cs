using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool time;
    public bool boss;

    public ObjectManager objManager;
    public GameObject player;
    public GameObject bossObject;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Transform joyUi;
    [SerializeField]
    private LevelUp levelUp;
    [SerializeField]
    public EnemySpawner enemySpawner;
    public float gameTime;
    public string distance;
    public float distanceNum;
    private float lastDistanceNum = 0f;
    public float maxDistance;
    public int levelUpCount = 0;
    public int bossCount = 1;

    private bool scoreStart = false;

    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600};

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void FixedUpdate()
    {
        Debug.Log("width : " + Screen.width);
        Debug.Log("height : " + Screen.height);

        // 임시 속도 조절 기능
        if (Input.GetKey(KeyCode.G))
        {
            Time.timeScale = 10;
        }
        else if(Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 1;
        }
        if(!time || boss)
        {
            return;
        }
        if(scoreStart)
        {
            gameTime += Time.deltaTime;
            distanceNum = (gameTime * 100);

            if (Mathf.FloorToInt(distanceNum / 5000) > Mathf.FloorToInt(lastDistanceNum / 5000) && !boss)
            {
                boss = true;
                float distanceNumQuotient = distanceNum / 5000;
                distanceNum = (int)distanceNumQuotient * 5000;
            }

            lastDistanceNum = distanceNum;
            distance = distanceNum.ToString("N0") + "M";
        }
    }

    public void ScoreStart()
    {
        scoreStart = true;
    }

    public void GameStart()
    {
        time = true;
        joyUi.localScale = Vector3.one;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoruotine());
    }

    IEnumerator GameOverCoruotine()
    {
        time = false;

        yield return new WaitForSeconds(0.1f);

        gameOverPanel.SetActive(true);
        Stop();
    }
    public void GetExp(int amount)
    {
        exp+= amount;

        if(exp >= nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            exp -= nextExp[level];
            level++;
            levelUpCount++;
            levelUp.ShowLevelUp();
        }
    }

    public void Stop()
    {
        time = false;
        Time.timeScale = 0;
        joyUi.localScale = Vector3.zero;

    }

    public void Resume()
    {
        time = true;
        Time.timeScale = 1;
        joyUi.localScale = Vector3.one;
    }
}
