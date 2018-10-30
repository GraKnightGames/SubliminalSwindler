using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuToGame : MonoBehaviour {
    [SerializeField] private Animator fadeAnim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Start"))
        {
            StartCoroutine(Fading());
        }
	}
    IEnumerator Fading()
    {
        fadeAnim.SetBool("Fading", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Level");
    }
}
