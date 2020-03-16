using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreScript : MonoBehaviour
{
    [SerializeField] ScoreHUDScript scoreHUD;

    [SerializeField] TMPro.TextMeshProUGUI yourScoreText, highScoreText;

    public void UpdateScore()
    {
        int playerScore = scoreHUD.GetScore();
        EvaluateAndSetHighScore(playerScore);
        UpdateDisplayedScores(playerScore, GetHighScore());
    }


    private const string HIGHSCOREPREFKEY = "HIGHSCORE";

    private int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGHSCOREPREFKEY, 0);
    }

    private void EvaluateAndSetHighScore(int score)
    {
        if(score > GetHighScore())
        {
            PlayerPrefs.SetInt(HIGHSCOREPREFKEY, score);
        }
    }

    private void UpdateDisplayedScores(int playerScore, int highScore)
    {
        yourScoreText.text = "Your Score: " + playerScore.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}
