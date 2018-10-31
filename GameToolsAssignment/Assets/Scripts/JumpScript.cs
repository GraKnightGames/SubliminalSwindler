using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour {
    private Rigidbody m_rb;
    private Animator m_playerAnim;
	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody>();
        m_playerAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }
	}
    IEnumerator Jump()
    {
        m_playerAnim.SetBool("Jump",true);
        m_rb.AddForce(0,630,0);
        yield return new WaitForSeconds(1.0f);
        m_rb.AddForce(0, -0.4f, 0);
        m_playerAnim.SetBool("Jump", false);
    }
}
