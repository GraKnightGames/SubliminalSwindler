using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAnimScript : MonoBehaviour
{
    private Animator m_anim;
    // Use this for initialization
    void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            StartCoroutine(cntrl());
        }
    }
    IEnumerator cntrl()
    {
        m_anim.SetBool("Controlling", true);
        yield return new WaitForSeconds(5.0f);
        m_anim.SetBool("Controlling", false);
    }
}

