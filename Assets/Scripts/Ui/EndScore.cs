using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI resultScoreText;
    [SerializeField]
    private TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        resultScoreText = GetComponent<TextMeshProUGUI>();

        int score = PlayerPrefs.GetInt("Score");
        int bestScore = PlayerPrefs.GetInt("BestScore");

        resultScoreText.text = "Score : " + score;
        bestScoreText.text = "Best : " + bestScore;
    }
}
