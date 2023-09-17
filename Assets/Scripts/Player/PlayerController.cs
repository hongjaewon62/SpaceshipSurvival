using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private KeyCode keyCodeAttack = KeyCode.Space;
    [SerializeField]
    private KeyCode keyCodeTestBoomAttack = KeyCode.Z;
    [SerializeField]
    private StageData stageData;
    private Movement movement;
    private BasicWeapon basicWeapon;
    private BoomWeapon boomWeapon;
    private PlayerHP playerHp;

    public int defence = 0;

    private int score;

    [SerializeField]
    private GameObject gameoverPanel;

    //public int Score { set => score = Mathf.Max(0, value); get => score; }

    private void Awake()
    {
        movement = GetComponent<Movement>();
        basicWeapon = GetComponent<BasicWeapon>();
        boomWeapon = GetComponent<BoomWeapon>();
        playerHp = GetComponent<PlayerHP>();
    }

    private void Update()
    {
        if(!GameManager.instance.time)
        {
            return;
        }

        if(!playerHp.dead)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            movement.Move(new Vector3(x, y, 0));

            if (Input.GetKeyDown(keyCodeAttack))
            {
                basicWeapon.StartFireing();
            }
            else if (Input.GetKeyUp(keyCodeAttack))
            {
                basicWeapon.StopFireing();
            }

            if(Input.GetKeyDown(keyCodeTestBoomAttack))
            {
                boomWeapon.StartCoroutine("Boom");
            }
        }
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.time)
        {
            return;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x), Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }

    public void DIe()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        string distance = (GameManager.instance.distance.TrimEnd('M').Replace(",",""));
        score = int.Parse(distance);
        PlayerPrefs.SetInt("Score", score);
        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        gameoverPanel.SetActive(true);
        GameManager.instance.Stop();
    }
}
