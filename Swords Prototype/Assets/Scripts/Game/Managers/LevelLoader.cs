using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] string nextLevel;
    public int sceneSelected;

    private void Start()
    {
        if (string.IsNullOrEmpty(nextLevel))
        {
            Debug.Break();
            Debug.LogError("Next Level field cannot be null!");
        }
    }

    public void LoadLevel()
    {
        GameManager.sceneName = nextLevel;
        FindObjectOfType<GameManager>().DoFadeAndSetLoadingState();
    }
}
