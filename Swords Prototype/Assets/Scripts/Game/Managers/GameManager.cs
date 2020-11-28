using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instace { get { return _instance; } }
    #endregion

    #region Game States
    public enum GameState { Paused, InGame, InMenu, Loading, LevelCompleted }
    public static GameState currentGameState { get; private set; }
    #endregion

    public static PlayerController player;
    public static string sceneName;
    public static Vector2 checkPoint;
    private FadeImage fadeScreen;
    private void Awake()
    {
        #region Initializing GameManager Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        #endregion
        currentGameState = GameState.InGame;
        fadeScreen = GetComponentInChildren<FadeImage>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if(currentGameState == GameState.InGame) { PauseGame(); }
            else { ResumeGame(); }
        }
    }

    #region Game Pause/Resume

    public void PauseGame()
    {
        currentGameState = GameState.Paused;
        if (player)
            player.playerAnimator.speed = 0f;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        currentGameState = GameState.InGame;
        if (player)
            player.playerAnimator.speed = 1f;
        Time.timeScale = 1f;   
    }
    #endregion

    #region Level Management

    public void DoFadeAndSetLoadingState()
    {
        currentGameState = GameState.Loading;
        fadeScreen.Fade(FadeImage.FadeMode.Show);
    }

    public void GoToLevel() => StartCoroutine(AsyncLoadLevel(sceneName));

    public IEnumerator AsyncLoadLevel(string levelName)
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(levelName);

        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }

        fadeScreen.Fade(FadeImage.FadeMode.Hide);
    }

    
    #endregion

    private void OnApplicationFocus(bool focus)
    {
        SendMessage(focus ? "ResumeGame" : "PauseGame");
    }

    public static void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        if (player)
            player.playerAnimator.speed = scale;
    }

    #region Set Game State
    public void SetMenuState() => currentGameState = GameState.InMenu;

    public void SetLoadingState() => currentGameState = GameState.Loading;

    public void SetInGameState() => currentGameState = GameState.InGame;
    public void SetPauseState() => currentGameState = GameState.Paused;

    public void SetState(GameState newState) => currentGameState = newState;

    #endregion
}
