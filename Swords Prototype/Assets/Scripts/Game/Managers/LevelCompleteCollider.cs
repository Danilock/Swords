using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LevelCompleteCollider : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.nextLevelName = nextLevel;
            FindObjectOfType<GameManager>().LevelComplete();
        }
    }
}
