using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingShotSound : MonoBehaviour {
    private AudioSource gunSFX;
	// Use this for initialization
	void Start () {
        gunSFX = GameObject.Find("GunSFX").GetComponent<AudioSource>(); //The Gun Sound
        StartCoroutine(Firing()); //Put in a coroutine to control timing of sounds
	}
	IEnumerator Firing()
    {
        gunSFX.Play();
        yield return new WaitForSeconds(0.03f);
        gunSFX.Stop();
    }

}
