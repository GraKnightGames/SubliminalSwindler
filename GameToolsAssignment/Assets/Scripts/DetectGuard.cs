using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGuard : MonoBehaviour
{
    [SerializeField] private GameObject m_otherGuard;
    private Animator m_otherGuardAnim;
    // Use this for initialization
    void Start()
    {
        m_otherGuardAnim = m_otherGuard.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Detector")
        {
            if (MindControl.control)
            {
                m_otherGuardAnim.SetBool("Firing", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
            m_otherGuardAnim.SetBool("Firing", false);
    }
}
