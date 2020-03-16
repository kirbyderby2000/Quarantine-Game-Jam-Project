using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCountdown : MonoBehaviour
{
    [SerializeField] MenuScript gameMenu;
    [SerializeField] int gameSeconds = 30;
    [SerializeField] int bonusSecondsReward = 10;
    [SerializeField] TMPro.TextMeshProUGUI countdownText;

    float currentSeconds;

    bool countdownStarted = false; 

    public static GameCountdown CountdownSingleton
    {
        private set;
        get;
    }

    private void Awake()
    {
        CountdownSingleton = this;
        currentSeconds = gameSeconds;
        UpdateCountdownText();
    }

    public void StartCountdown()
    {
        if (countdownStarted) return;

        currentSeconds = gameSeconds;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameSeconds", currentSeconds);
        countdownStarted = true;
        StartCoroutine(CountDownCoroutine());
    }

    IEnumerator CountDownCoroutine()
    {
        while(currentSeconds > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentSeconds--;
            UpdateCountdownText();
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameSeconds", currentSeconds);
            yield return null;
        }

        OnSecondsRanOut();

    }

    public void RewardBonusSeconds()
    {
        currentSeconds += bonusSecondsReward;
        GetComponent<Animator>().SetTrigger("growthTrigger");
        UpdateCountdownText();
    }

    private void OnSecondsRanOut()
    {
        gameMenu.EndOfGame();
    }

    private void UpdateCountdownText()
    {
        countdownText.text = currentSeconds.ToString();
    }
}
