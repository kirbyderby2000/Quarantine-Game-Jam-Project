using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] CanvasGroup gameHUD, startMenuHud, endMenuHud;
    [SerializeField] GameCountdown gameCountdown;
    [SerializeField] UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerController;
    [SerializeField] BulletSpawner bulletSpawner;
    [SerializeField] PlayerPull playerPull;
    [SerializeField] HighScoreScript highScoreDisplayer;
    private static bool replaySelected = false;

    public MenuScript MenuScriptSingleton
    {
        private set;
        get;
    }

    private void Awake()
    {
        MenuScriptSingleton = this;
        Cursor.visible = true;
        if (replaySelected) StartGame();
    }

    public void StartGame()
    {
        Cursor.visible = false;
        CanvasGroup menuCanvasGroup = this.GetComponent<CanvasGroup>();
        menuCanvasGroup.alpha = 0.0f;
        menuCanvasGroup.interactable = false;
        gameHUD.alpha = 1.0f;
        playerController.enabled = true;
        bulletSpawner.enabled = true;
        gameCountdown.StartCountdown();


    }

    public void ReloadLevel()
    {
        replaySelected = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void EndOfGame()
    {
        StartCoroutine(EndOfGameCoroutine());
    }
    public void ShowEndOfGameMenu()
    {
        highScoreDisplayer.UpdateScore();
        playerController.PullStarted();
        CanvasGroup menuCanvasGroup = this.GetComponent<CanvasGroup>();
        menuCanvasGroup.alpha = 1;
        menuCanvasGroup.interactable = true;
        playerController.enabled = false;
        bulletSpawner.enabled = false;
        
        playerPull.enabled = false;

        Destroy(playerPull);
        Destroy(playerController);
        Destroy(bulletSpawner);


        gameHUD.alpha = 0.0f;
        startMenuHud.alpha = 0.0f;
        startMenuHud.interactable = false;
        endMenuHud.alpha = 1.0f;
        endMenuHud.interactable = true;
        endMenuHud.blocksRaycasts = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    IEnumerator EndOfGameCoroutine()
    {
        playerController.ChangeCursorLockState(false);
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();

        ShowEndOfGameMenu();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        yield return null;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
