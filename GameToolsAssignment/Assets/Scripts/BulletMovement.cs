using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
    private Rigidbody m_rb;
    [SerializeField] private float m_force;
	// Use this for initialization
	void OnEnable () {
        m_rb = GetComponent<Rigidbody>();
        m_rb.AddForce(transform.forward * m_force);
        StartCoroutine(bulletKill());
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
    IEnumerator bulletKill()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
}
