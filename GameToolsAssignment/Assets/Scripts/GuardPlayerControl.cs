using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPlayerControl : MonoBehaviour {
    private Animator m_anim;
    private MindControl m_cntrl;
    [SerializeField] private AudioSource m_gunSFX;
    void Start()
    {
        m_cntrl = GetComponent<MindControl>();
        m_anim = GetComponent<Animator>();
    }
    //Player input
    public void Move(float turn, float forward)
    {
        print("Forward: " + forward);
        if (m_cntrl.m_controlledGuard = this.gameObject)
        {
                m_anim.SetFloat("Turn", turn);
                m_anim.SetFloat("Forward", forward);
            }
        }
    //Gun firing while controlling
    private void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            if(MindControl.control || RampGuardMindControl.control)
            {
                m_gunSFX.Play();
                m_anim.SetBool("isFiring", true);
            }
        }
        else
        {
            m_gunSFX.Stop();
            m_anim.SetBool("isFiring", false);
        }
    }
}

