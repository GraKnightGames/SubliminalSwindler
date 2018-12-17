using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ShotToGameOver : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        StartCoroutine(ChangingScene());
	}

    IEnumerator ChangingScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Lose");
    }
}
