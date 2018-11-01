using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GuardNormalMovement : MonoBehaviour
{
    private Animator m_guardAnim;
    private Transform m_firstWayPoint;

    private void Start()
    {
        m_guardAnim = GetComponent<Animator>();
        m_guardAnim.SetFloat("Forward", 1);
        m_firstWayPoint = GameObject.FindGameObjectWithTag("WP1").transform;
    }

    private void Update()
    {
        if(m_guardAnim.GetBool("Dying"))
        {
            m_guardAnim.SetFloat("Forward", 0);
            m_guardAnim.SetFloat("Turn", 0);
            m_guardAnim.SetLayerWeight(1, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WP1")
        {
            m_guardAnim.SetFloat("Forward", 0);
            m_guardAnim.SetFloat("Turn", -1);
        }
        if (other.tag == "WP2")
        {
            m_guardAnim.SetFloat("Forward", -1);
            m_guardAnim.SetFloat("Turn", 0);
        }
        if (other.tag == "WP3")
        {
            m_guardAnim.SetFloat("Forward", 0);
            m_guardAnim.SetFloat("Turn", 1);
        }
        if (other.tag == "WP4")
        {
            m_guardAnim.SetFloat("Forward", 1);
            m_guardAnim.SetFloat("Turn", 0);
        }
    }
   
}
