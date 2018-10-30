using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconPlayerAnimations : MonoBehaviour {
    [SerializeField] private GameObject m_thePlayer;
    private Animator m_playerAnim;
    private Animator m_thisAnim;
	// Use this for initialization
	void Start () {
        m_thisAnim = GetComponent<Animator>();
        m_playerAnim = m_thePlayer.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (MindControl.control)
        {
            m_thisAnim.SetBool("Controlling", true);
            print("Control Animation");
        }
        else if (!MindControl.control)
        {
            m_thisAnim.SetBool("Controlling", false);
        }
	}
}
