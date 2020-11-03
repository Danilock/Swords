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

    public enum GameState { Paused, InGame, InMenu, Loading }
    public static GameState currentGameState { get; private set; }

    public static PlayerController player;
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
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if(currentGameState == GameState.InGame) { PauseGame(); }
            else { ResumeGame(); }
        }
    }

    void PauseGame()
    {
        currentGameState = GameState.Paused;
        if (player)
            player.playerAnimator.speed = 0f;
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        currentGameState = GameState.InGame;
        if (player)
            player.playerAnimator.speed = 1f;
        Time.timeScale = 1f;   
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnApplicationFocus(bool focus)
    {
        SendMessage(focus ? "ResumeGame" : "PauseGame");
    }

    #region Set Game State
    public void SetMenuState() => currentGameState = GameState.InMenu;

    public void SetLoadingState() => currentGameState = GameState.Loading;

    public void SetInGameState() => currentGameState = GameState.InGame;
    public void SetPauseState() => currentGameState = GameState.Paused;
    #endregion
}
