using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastGuardsNormalMovement : MonoBehaviour {
    [SerializeField] private Animator m_guardAnim;
    private Transform m_firstWayPoint;

    private void Start()
    {
        m_guardAnim = GetComponent<Animator>();
        m_guardAnim.SetFloat("Forward", 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WP5")
        {
            if (this.tag != "Detector")
            {
                m_guardAnim.SetFloat("Forward", -1);
                m_guardAnim.SetFloat("Turn", 0);
            }
        }
        if (other.tag == "WP6")
        {
            if (this.tag != "Detector")
            {
                m_guardAnim.SetFloat("Forward", 0);
                m_guardAnim.SetFloat("Turn", 1);
            }
        }
        if (other.tag == "WP7")
        {
            if (this.tag != "Detector")
            {
                m_guardAnim.SetFloat("Forward", 1);
                m_guardAnim.SetFloat("Turn", 0);
            }
        }
        if (other.tag == "WP8")
        {
            if (this.tag != "Detector")
            {
                m_guardAnim.SetFloat("Forward", 0);
                m_guardAnim.SetFloat("Turn", -1);
            }
        }
    }
}

