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
    bool _inGame;
    public bool InGame { get { return _inGame; } private set { _inGame = value; } }
    bool _pause;
    public bool Pause { get { return _pause; }  private set { _pause = value; } }
    bool _canPauseGame;
    public bool CanPauseGame { get { return _canPauseGame; } set { _canPauseGame = value; } }
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
    }

    /*private void Update()
    {
        if (Input.GetButtonDown("Pause") && CanPauseGame)
        {
            if (!Pause) { PauseGame(); }
            else { ResumeGame(); }
        }
    }*/

    void PauseGame()
    {
        Pause = true;
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        Pause = false;
        Time.timeScale = 1f;   
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
