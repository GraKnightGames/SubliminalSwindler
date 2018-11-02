using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GuardKiller : MonoBehaviour {
    private Animator m_guardAnim;
    private bool m_inTrigger;
    [SerializeField] private CinemachineVirtualCamera m_guardCam;
    [SerializeField] private CinemachineVirtualCamera m_lastGuardCam;
    [SerializeField] private CinemachineVirtualCamera m_playerCam;
    private void Start()
    {
        m_guardAnim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GuardKiller")
        {
            m_inTrigger = true;
        }
    }
    private void Update()
    {
        if(m_inTrigger)
        {
            StartCoroutine(KillGuard());
        }
    }
    IEnumerator KillGuard()
    {
        m_guardAnim.SetLayerWeight(1, 0);
        m_guardAnim.SetBool("Dying", true);
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
        if(m_guardCam.enabled == true)
        {
            m_guardCam.enabled = false;
            m_playerCam.enabled = true;
        }
        if(m_lastGuardCam.enabled == true)
        {
            m_lastGuardCam.enabled = false;
            m_playerCam.enabled = true;
        }
    }
}
