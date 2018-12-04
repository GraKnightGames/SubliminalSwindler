using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
public class RampGuardMindControl : MonoBehaviour {
    private GameObject player;
    private PlayerMovement m_playerMoveScript;
    private RampGuardNormalMovement m_guardNorm;
    private NavMeshAgent m_agent;
    public static bool control;
    private bool m_inTrigger;
    [SerializeField] private ParticleSystem m_part;
    private Animator m_playerAnim;
    public GameObject m_controlledGuard;
    [SerializeField] private Animator m_guardAnim;
    private GuardPlayerControl m_guard;
    private float m_turn, m_forward;
    public static float waitTime;
    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] public CinemachineVirtualCamera guardCam;
    [SerializeField] private AudioSource m_mcAudio;
    [SerializeField] private Material mindControlIndMat;
    private Transform[] m_points;
    // Use this for initialization
    void Start()
    {
        waitTime = 10.0f;
        m_agent = GetComponent<NavMeshAgent>();
        m_guardNorm = GetComponent<RampGuardNormalMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
        m_playerMoveScript = player.GetComponent<PlayerMovement>();
        m_playerAnim = player.GetComponent<Animator>();
        m_guard = this.GetComponent<GuardPlayerControl>();
        m_guard.enabled = false;
        m_guardNorm.enabled = true;
        m_inTrigger = false;
        guardCam.enabled = false;
        m_controlledGuard = null;
        m_part.Pause();
        m_points = m_guardNorm.m_Waypoints;
    }

    private void Update()
    {
        m_turn = Input.GetAxis("Horizontal");
        m_forward = Input.GetAxis("Vertical");
        if (control)
        {
            m_playerAnim.SetBool("Controlling", true);
            m_controlledGuard = this.gameObject;
            m_guard.Move(m_turn, m_forward);
        }
        else if (!control)
        {
            m_playerAnim.SetBool("Controlling", false);
        }
        if (m_inTrigger)
        {
            mindControlIndMat.color = new Color(0.9f, 0, 0, 0.5f);
            if (Input.GetButtonDown("Start"))
            {
                print("Start Controlling");
                StartCoroutine(controlling());
            }
        }
        else if (!m_inTrigger)
        {
            mindControlIndMat.color = new Color(1, 1, 1, 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MindControl")
        {
            m_inTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MindControl")
        {
            m_inTrigger = false;
        }
    }
    IEnumerator controlling()
    {
        print("Cntrl");
        control = true;
        playerCam.enabled = false;
        guardCam.enabled = true;
        m_playerAnim.SetBool("Controlling", true);
        m_playerAnim.SetFloat("Forward", 0);
        m_playerAnim.SetFloat("Turn", 0);
        m_guardAnim.SetBool("BeingControlled", true);
        m_guardNorm.enabled = false;
        m_guard.enabled = true;
        m_mcAudio.Play();
        m_part.Play();
        yield return new WaitForSeconds(waitTime);
        m_guard.enabled = false;
        playerCam.enabled = true;
        guardCam.enabled = false;
        control = false;
        m_mcAudio.Stop();
        m_part.Stop();
        m_guardAnim.SetFloat("Forward", 0);
        m_guardAnim.SetFloat("Turn", 0);
        m_guardAnim.SetBool("BeingControlled", false);
        m_playerAnim.SetBool("Controlling", false);
        m_guardAnim.SetBool("isFiring", false);
        yield return new WaitForSeconds(1);
        m_guardNorm.enabled = true;
        m_guardNorm.m_NPCState = RampGuardNormalMovement.NPCState.PATROL;
        m_guardNorm.m_NavMeshAgent.SetDestination((m_points[m_guardNorm.m_CurrentWaypoint].position));
        m_guardNorm.HandleAnimation();
    }
}
