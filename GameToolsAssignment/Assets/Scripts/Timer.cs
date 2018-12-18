using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    [SerializeField] private Text m_text;
    private float m_time;
    private float repeatRate;
    [SerializeField] Animator fadeAnim;
    void Start()
    {
            StartCountdownTimer();
    }

    void StartCountdownTimer()
    {
        if (m_text != null)
        {
            m_time = 180; //Sets the time variable in seconds
            repeatRate = 0.0161f;
            m_text.text = "Time Left: 3:00";
            InvokeRepeating("UpdateTime", 0.0f, repeatRate); //Calls UpdateTime by the time indicated by repeatRate
        }
    }

    void UpdateTime()
    {
        if (m_text != null)
        {
            m_time -= Time.deltaTime; //Subtracts from time variable
            string minutes = Mathf.Floor(m_time / 60).ToString("00"); // Divides time into minutes and seconds
            string seconds = (m_time % 60).ToString("00");
            m_text.text = minutes + ":" + seconds; //Updates timer text to show minutes and seconds
        }
        if (m_time <= 0) //If the timer reaches 0, the player loses
        {
            m_time = 0;
            StartCoroutine(FadeToLose());
        }
    }
    IEnumerator FadeToLose()
    {
        fadeAnim.SetBool("FadeIn", true); //Fading a black square over the screen to simulate a complete fadeout
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Lose"); //Loading game over scene
    }
}