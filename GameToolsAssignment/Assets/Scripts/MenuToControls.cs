using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuToControls : MonoBehaviour
{
    [SerializeField] private Animator fadeAnim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(Fading());
        }
    }
    IEnumerator Fading()
    {
        fadeAnim.SetBool("Fading", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Controls");
    }
}
