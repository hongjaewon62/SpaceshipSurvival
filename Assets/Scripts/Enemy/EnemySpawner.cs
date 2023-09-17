using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class EnemyData
{
    public float spawnTime;
    public float health;
    public int damage;
    public int dropExp;
}
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private EnemyData[] enemyData;
    [SerializeField]
    private GameObject player;
    //[SerializeField]
    //private GameObject enemyPrefab;
    [SerializeField]
    private string[] enemyPrefab;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private int maxEnemyCount = 100;        // 현재 스테이지 최대 적
    [SerializeField]
    private float nextSpawnDelay;
    [SerializeField]
    private float currentSpawnDelay;
    [SerializeField]
    private GameObject bossWarningText;
    [SerializeField]
    private GameObject bossHpPanel;
    [SerializeField]
    private GameObject[] boss;
    private PlayerHP playerHp;

    [SerializeField]
    private ObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    //private int level;

    private void Awake()
    {
        spawnList = new List<Spawn>();
        enemyPrefab = new string[] { "Enemy1", "Enemy2", "Enemy3"};
        bossWarningText.SetActive(false);
        bossHpPanel.SetActive(false);
        for(int i = 0; i < boss.Length; i++)
        {
            Debug.Log("보스" + i);
            boss[i].SetActive(false);
        }
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
        ReadSpawnFile();
    }

    //private void Start()
    //{
    //    StartCoroutine("SpawnEnemy");
    //}

    private void Update()
    {
        if (!GameManager.instance.time)
        {
            return;
        }
        //level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), enemyData.length - 1);
        if (playerHp.dead)
        {
            StopCoroutine("SpawnEnemy");
        }
    }

    public void GameStart()
    {
        StartCoroutine("SpawnEnemy");
    }

    private void ReadSpawnFile()
    {
        // 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // 파일 읽기
        TextAsset textFile = Resources.Load("SpawnData") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);
            if(line == null)
            {
                break;
            }


            // 데이터 생성
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = float.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        // 파일 닫기
        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }

    private IEnumerator SpawnEnemy()
    {
        int currentEnemyCount = 0;      // 적 생성 숫자 카운트용 변수
        while(true)
        {
            int enemyIndex = 0;
            switch (spawnList[spawnIndex].type)
            {
                case "1":
                    enemyIndex = 0;
                    break;
                case "2":
                    enemyIndex = 1;
                    break;
                case "3":
                    enemyIndex = 2;
                    break;
            }
            //int randomEnemy = Random.Range(0, 2);
            //float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            //GameObject enemy = objectManager.MakeObject(enemyPrefab[randomEnemy]);
            //enemy.transform.position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0f);

            float enemySpawnPoint = spawnList[spawnIndex].point;

            GameObject enemy = objectManager.MakeObject(enemyPrefab[enemyIndex]);
            enemy.GetComponent<Enemy>().player = player;
            enemy.transform.position = new Vector3(enemySpawnPoint, stageData.LimitMax.y + 1.0f, 0f);
            //enemy.GetComponent<Enemy>().EnemyDataInit(enemyData[level]);
            //enemy.GetComponent<EnemyHp>().EnemyDataInit(enemyData[level]);

            currentEnemyCount++;

            // 적을 최대 숫자까지 생성하면 적 생성 코루틴 중지, 보스 생성 코루틴 실행
            if(currentEnemyCount == maxEnemyCount)
            {
                StartCoroutine("SpawnBoss");
                break;
            }

            spawnIndex++;
            if(spawnIndex == spawnList.Count)
            {
                StopCoroutine("SpawnEnemy");
                yield return null;
            }

            nextSpawnDelay = spawnList[spawnIndex].delay;

            //yield return new WaitForSeconds(spawnTime);
            yield return new WaitForSeconds(nextSpawnDelay);
        }
    }

    private IEnumerator SpawnBoss()
    {
        bossWarningText.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        bossWarningText.SetActive(false);

        for(int i = 0; i < boss.Length; i++)
        {
            Debug.Log("보스");
            // if문으로 미터마다 보스 활성화
            bossHpPanel.SetActive(true);
            boss[i].SetActive(true);
            boss[i].GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
        }
    }
}
