using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUp : MonoBehaviour {
    [SerializeField] private Animator fadeBoxAnim;
	// Use this for initialization
	void Start () {
        StartCoroutine(Fade());
	}
	IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.4f);
        fadeBoxAnim.SetBool("Fade", true);
    }
}
