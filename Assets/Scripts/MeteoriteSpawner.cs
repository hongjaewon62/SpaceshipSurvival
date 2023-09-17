using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject warningLinePrefab;
    [SerializeField]
    private GameObject meteoritePrefab;
    [SerializeField]
    private float minSpawnTime = 1f;
    [SerializeField]
    private float maxSpawnTime = 4f;

    private PlayerHP playerHp;
    [SerializeField]
    private ObjectManager objectManager;

    private void Awake()
    {
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
        if(!playerHp.dead)
        {
            StartCoroutine("SpawnMeteorite");
        }
    }

    private void Update()
    {
        if (!GameManager.instance.time)
        {
            StopCoroutine("SpawnMeteorite");
            return;
        }

        if (playerHp.dead)
        {
            StopCoroutine("SpawnMeteorite");
        }
    }
    private IEnumerator SpawnMeteorite()
    {
        while(true)
        {
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);

            //GameObject warningLineObject = Instantiate(warningLinePrefab, new Vector3(positionX, 0, 0), Quaternion.identity);
            GameObject warningLineObject = objectManager.MakeObject("WarningLine");
            warningLineObject.transform.position = new Vector3(positionX, 0, 0);

            yield return new WaitForSeconds(1.0f);

            //Destroy(warningLineObject);

            warningLineObject.SetActive(false);

            Vector3 meteoritePosition = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0);
            GameObject meteoriteObject = objectManager.MakeObject("Meteorite");
            meteoriteObject.transform.position = meteoritePosition;
            //Instantiate(meteoritePrefab, meteoritePosition, Quaternion.identity);

            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
