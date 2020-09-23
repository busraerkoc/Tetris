using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SoundControl : MonoBehaviour
{
    public bool musicEnabled = true;
    public IconToggle musicIconToggle;
    public AudioSource audioSource;
    public int state;
    // Start is called before the first frame update

    void Awake()
    {
        PlayerPrefs.SetInt("Music", musicEnabled ? 1 : 0);
    }
    void Start()
    {
        PlayBackgroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBackgroundMusic()
    {
        if(PlayerPrefs.GetInt("Music", 1) > 0) 
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
    public void ToggleMusic()
	{
		musicEnabled = !musicEnabled;
        PlayerPrefs.SetInt("Music", musicEnabled ? 1 : 0);
		if (musicIconToggle)
		{
			musicIconToggle.ToggleIcon(musicEnabled);
            PlayBackgroundMusic();
		}
	}	

}
