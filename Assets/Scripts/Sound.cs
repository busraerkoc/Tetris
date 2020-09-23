using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        PlayBackgroundMusic();
    }
    public void PlayBackgroundMusic()
    {
        if(PlayerPrefs.GetInt("Music", 0) > 0)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

}
