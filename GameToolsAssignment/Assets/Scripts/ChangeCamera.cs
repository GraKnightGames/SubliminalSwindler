using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera inCam;
    [SerializeField] private CinemachineVirtualCamera outCam;
    private void Start()
    {
        outCam.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CamChange")
        {
            inCam.enabled = false;
            outCam.enabled = true;
        }
    }
}