using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardKiller : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GuardKiller")
        {
            Destroy(this.gameObject);
        }
    }
}
