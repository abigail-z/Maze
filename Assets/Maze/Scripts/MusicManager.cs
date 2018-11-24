using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public float minVolumeDistance;
    public float minVolume;
    public AudioClip daySong;
    public AudioClip nightSong;
    private AudioSource music;
    private bool fog;

	// Use this for initialization
	void Start ()
    {
        music = GetComponent<AudioSource>();
        music.clip = daySong;
        music.Play();
        ShaderManager.Instance.StateChange += FogVolume;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = (player.position - enemy.position).magnitude;
        music.volume = minVolume + (1 - minVolume) * Mathf.Clamp01(1 - distance / minVolumeDistance);

        if (fog)
        {
            music.volume *= 0.5f;
        }

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

    void FogVolume ()
    {
        string name = ShaderManager.Instance.CurrentShader.name;
        if (name == "Custom/Fog")
        {
            fog = true;
        }
        else
        {
            fog = false;
        }

        if (name == "Custom/Night")
        {
            music.Stop();
            music.clip = nightSong;
            music.Play();
        }
        else if (music.clip == nightSong)
        {
            music.Stop();
            music.clip = daySong;
            music.Play();
        }
    }
}
