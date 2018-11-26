using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GrateTele : MonoBehaviour {
    [SerializeField] Transform m_teleTrans;
    [SerializeField] Transform m_destTrans;
    [SerializeField] private CinemachineVirtualCamera m_playerCam;
    [SerializeField] private CinemachineVirtualCamera m_newPlayerCam;
    private Transform m_player;
    private bool m_inTele;
	// Use this for initialization
	void Start () {
        m_inTele = false;
        m_player = GetComponent<Transform>();
	}

    private void Update()
    {
        if(m_inTele)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                m_playerCam.enabled = false;
                m_newPlayerCam.enabled = true;
                m_player.position = m_destTrans.position;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tele")
        {
            m_inTele = true;
        }
    }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Tele")
            {
                m_inTele = false;
            }
        }
    }
