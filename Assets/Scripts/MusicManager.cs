using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public float minVolumeDistance;
    private AudioSource music;

	// Use this for initialization
	void Start ()
    {
        music = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = (player.position - enemy.position).magnitude;
        music.volume = 1 - distance / minVolumeDistance;

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
