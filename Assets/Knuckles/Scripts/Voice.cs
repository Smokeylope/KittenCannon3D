using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice : MonoBehaviour {
    public List<AudioClip> clips;
    private AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
        UnityEngine.Random.InitState((int) DateTime.Now.Ticks);
	}
	
	// Update is called once per frame
	void Update () {
		if (!source.isPlaying)
        {
            source.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Count)]);
        }
	}
}
