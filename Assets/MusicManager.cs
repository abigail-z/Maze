using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource music;

	// Use this for initialization
	void Start ()
    {
        music = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("Music"))
        {
            if (music.isPlaying)
            {
                music.Stop();
            }
            else
            {
                music.Play();
            }
        }
	}
}
