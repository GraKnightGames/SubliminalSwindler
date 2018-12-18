using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeIn : MonoBehaviour {
    private AudioSource m_music;
	// Use this for initialization
	void Start () {
        m_music = this.GetComponent<AudioSource>();
        m_music.volume = 0; //Starting audio volume from 0 for scene transition
	}
	
	// Update is called once per frame
	void Update () {
        if (m_music.volume < 0.7f)
        {
            m_music.volume += 0.01f; //Increasing volume to fade in to scene
        }
	}
}
