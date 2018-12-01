using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class StartOfGame : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera m_goalCam;
    [SerializeField] private CinemachineVirtualCamera m_midCam;
    [SerializeField] private CinemachineVirtualCamera m_playerCam;
    [SerializeField] private Animator m_iconAnim;
    [SerializeField] private Animator m_borderAnim;
    [SerializeField] private Animator m_fadeAnim;
    [SerializeField] private Text m_text;
    // Use this for initialization
    void Start () {
        m_text.enabled = false;
        m_playerCam.enabled = false;
        m_goalCam.enabled = true;
        StartCoroutine(startView());
        m_iconAnim.SetBool("FadingIn", false);
        m_borderAnim.SetBool("BorderFading", false);
        m_fadeAnim.SetBool("FadeOut", true);
	}

    IEnumerator startView()
    {
        yield return new WaitForSeconds(2.0f);
        m_midCam.enabled = true;
        m_goalCam.enabled = false;
        yield return new WaitForSeconds(2);
        m_midCam.enabled = false;
        m_playerCam.enabled = true;
        yield return new WaitForSeconds(0.5f);
        m_iconAnim.SetBool("FadingIn", true);
        m_borderAnim.SetBool("BorderFading", true);
        m_text.enabled = true;
    }
}
