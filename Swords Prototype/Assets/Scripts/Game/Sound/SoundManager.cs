using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager _instance;
    public static SoundManager Instace { get { return _instance; } }
    #endregion

    [SerializeField] AudioClip currentSong;
    [SerializeField] AudioSource audioSource;
    Animator soundAnimator;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        audioSource = GetComponent<AudioSource>();
        soundAnimator = GetComponent<Animator>();
    }

    public void FadeSound(bool fadeValue)
    {
        soundAnimator.SetBool("Hide", fadeValue);
    }

    public void ChangeSong(AudioClip newSong)
    {
        currentSong = newSong;
        audioSource.clip = newSong;
        audioSource.Play();
    }
}
