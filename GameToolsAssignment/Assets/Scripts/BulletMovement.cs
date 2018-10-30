using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
    private Rigidbody m_rb;
    private float m_force;
	// Use this for initialization
	void OnEnable () {
        m_rb = GetComponent<Rigidbody>();
        m_rb.AddForce(Vector3.forward * m_force);
    }
}
