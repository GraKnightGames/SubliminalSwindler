using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGun : MonoBehaviour {
    private float m_maxDamage = 120f;
    private float m_minDamage = 45f;
    private static bool m_firing;
    private Animator m_guardAnim;
    private LineRenderer m_shotLine;
    private BoxCollider m_col;
    private Transform player;
    private float m_scaledDamage;
    [SerializeField] private GameObject m_guard;
    private void Start()
    {
        m_shotLine = GetComponentInChildren<LineRenderer>();
        m_guardAnim = m_guard.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        m_scaledDamage = m_maxDamage - m_minDamage;
    }
    // Update is called once per frame
    void Update () {
		if(m_firing)
        {
            Shoot();
        }
	}
    void Shoot()
    {
        m_guardAnim.SetBool("isFiring", true);
    }
}
