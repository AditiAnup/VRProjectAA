using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    public AudioSource adsrc;
    public AudioClip[] audioClips;
    private int nowPlaying = -1;
    
    void Start()
    {
        adsrc = GetComponent<AudioSource>();
        playAudio(1);
    }

    public void stopAudio()
    {
        adsrc.Pause();
        nowPlaying = -1;
    }
    public void playAudio(int i)
    {
        if (nowPlaying!=i)
        {
            if(adsrc.isPlaying)
            {
                adsrc.Pause();
            }
            adsrc.clip = audioClips[i];
            adsrc.Play();
            nowPlaying = i;
        }
    }
}
