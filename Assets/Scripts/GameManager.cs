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
        //if (Input.GetKey(KeyCode.G))
        //{
        //    Time.timeScale = 10;
        //}
        //else if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Time.timeScale = 1;
        //}
        if (!time || boss)
        {
            return;
        }
        if(scoreStart)
        {
            gameTime += Time.deltaTime;
            distanceNum = (gameTime * 100);

            // 점수가 5000일때마다 보스 등장
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


    // 점수 시작
    public void ScoreStart()
    {
        scoreStart = true;
    }

    // 게임 시작 - 시간, 조이스틱 활성화
    public void GameStart()
    {
        time = true;
        joyUi.localScale = Vector3.one;
    }

    // 게임 오버 - 게임 종료시 GameOverCoruotine 코투린 실행
    public void GameOver()
    {
        StartCoroutine(GameOverCoruotine());
    }

    // 시간을 멈추고, 게임 오버 패널 활성화
    IEnumerator GameOverCoruotine()
    {
        time = false;

        yield return new WaitForSeconds(0.1f);

        gameOverPanel.SetActive(true);
        Stop();
    }

    // 경험치 획득 함수
    public void GetExp(int amount)
    {
        exp+= amount;

        // 경험치가 최대값을 넘지 못하게 방지
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


    // 게임 멈춤 - 설정, 레벨업 사용
    public void Stop()
    {
        // 시작하기 전에 비활성화
        if(startWindow.activeSelf == false)
        {
            time = false;
            Time.timeScale = 0;
            joyUi.localScale = Vector3.zero;
        }
    }

    // 게임 진행
    public void Resume()
    {
        // 시작하기 전에 비활성화
        if(startWindow.activeSelf == false)
        {
            time = true;
            Time.timeScale = 1;
            joyUi.localScale = Vector3.one;
        }
    }

    // 화면 해상도에 따라 위치가 달라지도록 사이즈를 변경
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

    // 게임 종료
    public void QuitGame()
    {
        Application.Quit();
    }
}
