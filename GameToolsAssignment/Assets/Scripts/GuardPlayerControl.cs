using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPlayerControl : MonoBehaviour {
    private Animator m_anim;

    void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    public void Move(float turn, float forward)
    {
        m_anim.SetFloat("Turn", turn);
        m_anim.SetFloat("Forward", forward);
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            m_anim.SetBool("isFiring", true);
        }
        else
        {
            m_anim.SetBool("isFiring", false);
        }
    }
}

