using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class EnemyData
{
    public float spawnTime;
    public float health;
    public float damage;
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
    private int maxEnemyCount = 100;        // ���� �������� �ִ� ��
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
    public int randomBossIndex;
    private PlayerHP playerHp;

    [SerializeField]
    private ObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    private string[] spawnData;
    //private int level;

    private void Awake()
    {
        spawnList = new List<Spawn>();
        enemyPrefab = new string[] { "Enemy1", "Enemy2", "Enemy3", "Enemy4", "Enemy5", "Enemy6" };
        bossWarningText.SetActive(false);
        bossHpPanel.SetActive(false);
        for(int i = 0; i < boss.Length; i++)
        {
            Debug.Log("����" + i);
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

        if (playerHp.dead)
        {
            StopCoroutine("SpawnEnemy");
        }
    }

    public void GameStart()
    {
        StartCoroutine("SpawnEnemy");
    }

    // ������ �о� �� ��ȯ
    private void ReadSpawnFile()
    {
        spawnData = new string[] { "SpawnData1", "SpawnData2", "SpawnData3" , "SpawnData4"};
        int randomNum;
        randomNum = Random.Range(0, spawnData.Length);
        // �ʱ�ȭ
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // ���� �б�
        TextAsset textFile = Resources.Load("EnemyData/" + spawnData[randomNum]) as TextAsset;
        //TextAsset textFile = Resources.Load("EnemyData/SpawnData5") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);
            if(line == null)
            {
                break;
            }


            // ������ ����
            Spawn spawnData = new Spawn();
            string[] splitValues = line.Split(',');
            spawnData.delay = float.Parse(splitValues[0]);      // ������
            spawnData.type = splitValues[1];                    // �� Ÿ��
            spawnData.xPoint = float.Parse(splitValues[2]);     // x��ǥ
            spawnData.yPoint = splitValues.Length >= 4 ? float.Parse(splitValues[3]) : stageData.LimitMax.y + 1.0f;     // y ��ǥ
            spawnList.Add(spawnData);
        }

        // ���� �ݱ�
        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }

    private IEnumerator SpawnEnemy()
    {
        int currentEnemyCount = 0;      // �� ���� ���� ī��Ʈ�� ����
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
                case "4":
                    enemyIndex = 3;
                    break;
                case "5":
                    enemyIndex = 4;
                    break;
                case "6":
                    enemyIndex = 5;
                    break;
            }

            // �ػ󵵿� �°� x ��ǥ ����
            if(spawnList[spawnIndex].xPoint > 0)
            {
                spawnList[spawnIndex].xPoint += (Mathf.Abs(stageData.limitMin.x) - 2.8f);
                Debug.Log("1 : " + spawnList[spawnIndex].xPoint);
            }
            else if (spawnList[spawnIndex].xPoint < 0)
            {
                spawnList[spawnIndex].xPoint -= (Mathf.Abs(stageData.limitMin.x) - 2.8f);
                Debug.Log("2 : " + spawnList[spawnIndex].xPoint);
            }
            float enemyXSpawnPoint = spawnList[spawnIndex].xPoint;
            float enemyYSpawnPoint = spawnList[spawnIndex].yPoint;

            GameObject enemy = objectManager.MakeObject(enemyPrefab[enemyIndex]);

            // 3�� �� ��ġ ����
            if (enemyIndex == 3 && enemyXSpawnPoint > 0)
            {
                enemy.transform.rotation = Quaternion.Euler(0, 0, 90);
                enemy.GetComponent<Movement>().moveDirection = new Vector3(-1, -0.2f, 0);
            }
            else if (enemyIndex == 3 && enemyXSpawnPoint < 0)
            {
                enemy.transform.rotation = Quaternion.Euler(0, 0, -90);
                enemy.GetComponent<Movement>().moveDirection = new Vector3(1, -0.2f, 0);
            }
            Debug.Log(enemy);
            enemy.GetComponent<Enemy>().player = player;
            enemy.transform.position = new Vector3(enemyXSpawnPoint, enemyYSpawnPoint, 0f);

            currentEnemyCount++;

            // ���� ȣ��
            if(GameManager.instance.boss)
            {
                StartCoroutine(SpawnBoss());
                StopCoroutine(SpawnEnemy());
                break;
            }

            spawnIndex++;
            if(spawnIndex == spawnList.Count)
            {
                //StopCoroutine("SpawnEnemy");
                spawnIndex = 0;
                ReadSpawnFile();
                yield return null;
            }

            nextSpawnDelay = spawnList[spawnIndex].delay;

            //yield return new WaitForSeconds(spawnTime);
            yield return new WaitForSeconds(nextSpawnDelay);
        }
    }

    // ���� ��ȯ
    private IEnumerator SpawnBoss()
    {
        // ��� �޽���
        bossWarningText.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        bossWarningText.SetActive(false);

        // ������ ���� ����
        randomBossIndex = Random.Range(0, boss.Length);

        bossHpPanel.SetActive(true);
        boss[randomBossIndex].SetActive(true);
        boss[randomBossIndex].GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }
}
