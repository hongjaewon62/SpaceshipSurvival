using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool time;

    public GameObject player;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Transform joyUi;
    [SerializeField]
    private LevelUp levelUp;
    public float gameTime;
    public string distance;
    public float maxDistance;
    public int levelUpCount = 0;

    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600};

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if(!time)
        {
            return;
        }

        gameTime += Time.deltaTime;
        //distance = Mathf.Round(distance + Time.deltaTime * 100f);
        distance = (gameTime * 100).ToString("N0") + "M";
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
    public void GetExp()
    {
        exp++;

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
