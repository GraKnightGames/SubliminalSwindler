using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour
{
    private float m_turn;
    private float m_forward;
    private PlayerMovement m_play;
    private GuardPlayerControl m_guard;
    // Use this for initialization
    void Start()
    {
        m_play = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
            m_turn = Input.GetAxis("Horizontal");
            m_forward = Input.GetAxis("Vertical");
        if (!MindControl.control)
        {
            m_play.Move(m_turn, m_forward);
        }
    }
}
