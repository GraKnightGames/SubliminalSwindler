using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeLeftScript : MonoBehaviour {
    [SerializeField] private Image barImg;
    [SerializeField] private float m_fillAmount;
    [SerializeField] private MindControl m_mc;
    private MindControl m_cntrl;
    private float m_decAmount = 0.02096f;
    private float startControl;
    private float control;
	// Use this for initialization
	void Start () {
        startControl = 10;
        control = startControl;
        m_fillAmount = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (MindControl.control)
        {
            startControl -= m_decAmount;
        }
            else if(!MindControl.control)
            {
            startControl = 10.0f;
            }
        Bar();
	}
    void Bar()
    { 
            barImg.fillAmount = startControl / control;
    }
}
