using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChanger : MonoBehaviour
{
    [SerializeField] AudioClip newSong;
    SoundManager soundMaster;

    private void Start()
    {
        soundMaster = FindObjectOfType<SoundManager>();
        soundMaster.ChangeSong(newSong);
    }
}
