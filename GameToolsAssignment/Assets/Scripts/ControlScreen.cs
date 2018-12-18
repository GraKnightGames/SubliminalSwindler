using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlScreen : MonoBehaviour {
    [SerializeField] private Animator m_blackBoxAnim;
	// Use this for initialization
	void Start () {
        StartCoroutine(Fading());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(FadingOut());
        }
	}
    IEnumerator Fading()
    {
        yield return new WaitForSeconds(0.4f);
        m_blackBoxAnim.SetBool("FadeOut", true);
    }
    IEnumerator FadingOut()
    {
        m_blackBoxAnim.SetBool("FadeOut", false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }
}
