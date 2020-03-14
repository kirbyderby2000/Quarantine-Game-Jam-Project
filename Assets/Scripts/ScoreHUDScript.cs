using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHUDScript : MonoBehaviour
{
    public static ScoreHUDScript ScoreHUDSingleton
    {
        private set;
        get;
    }
    
    [SerializeField]
    private TextMeshProUGUI scoreHUDText;
    [SerializeField]
    private Animator animator;

    private int currentScore;

    private void Awake()
    {
        ScoreHUDSingleton = this;
    }

    public void PointsReceived(int pointsReceived = 1)
    {
        currentScore += pointsReceived;
        scoreHUDText.text = currentScore.ToString();

        animator.SetTrigger("growthTrigger");
    }
}
