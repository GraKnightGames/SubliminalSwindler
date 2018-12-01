using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    [SerializeField] private Text m_text;
    private float m_time;
    [SerializeField] Animator fadeAnim;
    void Start()
    {
            StartCountdownTimer();
    }

    void StartCountdownTimer()
    {
        if (m_text != null)
        {
            m_time = 180;
            m_text.text = "Time Left: 3:00";
            InvokeRepeating("UpdateTime", 0.0f, 0.0161f);
        }
    }

    void UpdateTime()
    {
        if (m_text != null)
        {
            m_time -= Time.deltaTime;
            string minutes = Mathf.Floor(m_time / 60).ToString("00");
            string seconds = (m_time % 60).ToString("00");
            m_text.text = minutes + ":" + seconds;
        }
        if (m_time <= 0)
        {
            m_time = 0;
            StartCoroutine(FadeToLose());
        }
    }
    IEnumerator FadeToLose()
    {
        fadeAnim.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Lose");
    }
}