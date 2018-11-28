using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class DoorOpen : MonoBehaviour {
    [SerializeField] private GameObject m_theDoor;
    [SerializeField] private Canvas m_myCanvas;
    [SerializeField] private GameObject m_theGuard;
    [SerializeField] private CinemachineVirtualCamera m_playerCam;
    [SerializeField] private CinemachineVirtualCamera m_doorCam;
    private Animator m_eAnim;
    private Animator m_doorAnim;
    private Animator m_guardAnim;
    [SerializeField] private float waitTime;
    private bool m_inTrigger;
    private MindControl m_cntrl;
    private void Start()
    {
        m_doorAnim = m_theDoor.GetComponent<Animator>();
        m_eAnim = m_myCanvas.GetComponentInChildren<Animator>();
        m_guardAnim = m_theGuard.GetComponent<Animator>();
        m_doorCam.enabled = false;
    }

    private void Update()
    {
        if (m_inTrigger)
        {
            if (MindControl.control)
            {
                m_eAnim.SetBool("FadingIn", true);
                m_playerCam.enabled = false;
                m_doorCam.enabled = true;
                if (Input.GetKeyDown("e"))
                {
                    StartCoroutine(doorOpeningAndClosing());
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Door")
        {
            m_inTrigger = true;   
        }
    }
    private void OnTriggerExit(Collider other)
    {
        m_inTrigger = false;
    }

    IEnumerator doorOpeningAndClosing()
    {
        m_guardAnim.SetBool("isOpening", true);
        m_eAnim.SetBool("FadingIn", false);
        m_doorAnim.SetBool("Opening", true);
        yield return new WaitForSeconds(waitTime);
        m_guardAnim.SetBool("isOpening", false);
        m_doorAnim.SetBool("Opening", false);
    }
}
