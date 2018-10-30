using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenLight : MonoBehaviour {
    private Light m_light;
    private float m_waitTime;
    private float m_startIntensity;
    private void Start()
    {
        m_light = GetComponentInChildren<Light>();
        m_startIntensity = m_light.intensity;
    }
    // Update is called once per frame
    void Update () {
        m_waitTime = Random.Range(0f, 20f);
        StartCoroutine(Flickering());
        print(m_waitTime);
	}
    IEnumerator Flickering()
    {
        yield return new WaitForSeconds(m_waitTime);
        m_light.intensity = 9.0f;
        yield return new WaitForSeconds(0.8f);
        m_light.intensity = m_startIntensity;
    }
}
