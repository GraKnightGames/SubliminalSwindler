using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField] private Text m_text;
    private float m_time;
    void Start()
    {
            StartCoundownTimer();
    }

    void StartCoundownTimer()
    {
        if (m_text != null)
        {
            m_time = 180;
            m_text.text = "Time Left: 20:00:000";
            InvokeRepeating("UpdateTime", 0.0f, 0.01667f);
        }
    }

    void UpdateTime()
    {
        if (m_text != null)
        {
            m_time -= Time.deltaTime * 0.5f;
            string minutes = Mathf.Floor(m_time / 60).ToString("00");
            string seconds = (m_time % 60).ToString("00");
            m_text.text = minutes + ":" + seconds;
        }
    }
}