using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrateTele : MonoBehaviour {
    [SerializeField] Transform m_teleTrans;
    [SerializeField] Transform m_destTrans;
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
                m_inTele = true;
            }
        }
    }
