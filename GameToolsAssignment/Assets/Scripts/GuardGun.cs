using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGun : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GameObject m_bullet;
    [SerializeField] private GameObject m_guard;
    private Animator m_guardAnim;
    [SerializeField] private Transform m_bulletSpawn;
    private void Start()
    {
        m_guardAnim = m_guard.GetComponent<Animator>();
    }
    private void Update()
    {
        if(m_guardAnim.GetBool("isFiring"))
        {
            Instantiate(m_bullet, m_bulletSpawn.position, m_bulletSpawn.rotation);
        }
    }
}