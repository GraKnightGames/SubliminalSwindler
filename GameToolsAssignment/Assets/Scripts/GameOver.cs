using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour {
    private GuardNormalMovement m_guard;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(m_guard.m_NPCState == GuardNormalMovement.NPCState.CHASE)
        {
            StartCoroutine(UntilGO());
        }
	}
    IEnumerator UntilGO()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("BeingShot");
    }
}
