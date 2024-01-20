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
    public StageData stageData;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Transform joyUi;
    [SerializeField]
    private LevelUp levelUp;
    [SerializeField]
    public EnemySpawner enemySpawner;
    [SerializeField]
    private GameObject startWindow;
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
        ScreenSize();
    }

    private void FixedUpdate()
    {
        // 임시 속도 조절 기능
        if (Input.GetKey(KeyCode.G))
        {
            Time.timeScale = 10;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 1;
        }
        if (!time || boss)
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

        if (exp >= nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            while(nextExp[Mathf.Min(level, nextExp.Length - 1)] <= exp )
            {
                exp -= nextExp[Mathf.Min(level, nextExp.Length - 1)];
                level++;
                levelUpCount++;
                levelUp.ShowLevelUp();
            }
        }
    }

    public void Stop()
    {
        if(startWindow.activeSelf == false)
        {
            time = false;
            Time.timeScale = 0;
            joyUi.localScale = Vector3.zero;
        }
    }

    public void Resume()
    {
        if(startWindow.activeSelf == false)
        {
            time = true;
            Time.timeScale = 1;
            joyUi.localScale = Vector3.one;
        }
    }

    private void ScreenSize()
    {
        // 카메라의 뷰포트 좌표 구하기
        Vector3 topRightViewport = new Vector3(1f, 1f, Camera.main.nearClipPlane);
        Vector3 bottomLeftViewport = new Vector3(0f, 0f, Camera.main.nearClipPlane);

        // 뷰포트 좌표를 월드 좌표로 변환
        Vector3 topRightWorld = Camera.main.ViewportToWorldPoint(topRightViewport);
        Vector3 bottomLeftWorld = Camera.main.ViewportToWorldPoint(bottomLeftViewport);

        Debug.Log("Top Right World Coordinates: " + topRightWorld);
        Debug.Log("Bottom Left World Coordinates: " + bottomLeftWorld);
        stageData.limitMin.x = bottomLeftWorld.x;
        stageData.limitMin.y = bottomLeftWorld.y + 0.8f;

        stageData.limitMax.x = topRightWorld.x;
        stageData.limitMax.y = topRightWorld.y;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
