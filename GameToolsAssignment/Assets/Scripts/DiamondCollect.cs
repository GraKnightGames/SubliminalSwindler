using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DiamondCollect : MonoBehaviour {
    [SerializeField] private Light m_aboveLight;
    private GameObject m_theDiamond;
    private bool m_inTrigger;
    private bool m_goalCollected;
    [SerializeField] private Animator m_fadeAnim;
    [SerializeField] private AudioSource m_alarmSFX;
	// Use this for initialization
	void Start () {
        m_theDiamond = GameObject.FindGameObjectWithTag("Diamond");
        m_goalCollected = false;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Goal")
        {
            print("Should be triggering");
            m_inTrigger = true;
        }
        else
        {

        }
    }
    private void Update()
    {
        if(m_inTrigger)
        {
            print("inTrigger");
            if (Input.GetKeyDown("e"))
            {
                m_theDiamond.SetActive(false);
                m_aboveLight.color = Color.red;
                StartCoroutine(WinScreen());
            }
        }
    }
    IEnumerator WinScreen()
    {
        m_fadeAnim.SetBool("FadeIn", true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Win");
    }
}
