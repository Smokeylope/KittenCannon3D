using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour {

    public AudioSource bgMusic;

    public void SwitchOnOff()
    {
        if (bgMusic.isPlaying)
        {
            bgMusic.Stop();
        } else
        {
            bgMusic.Play();
        }
    }
}
