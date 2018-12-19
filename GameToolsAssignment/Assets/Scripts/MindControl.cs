using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;
public class MindControl : MonoBehaviour {
    private GameObject player;
    private PlayerMovement m_playerMoveScript;
    [SerializeField] private GuardNormalMovement m_guardNorm;
    private NavMeshAgent m_agent;
    public static bool control;
    private bool m_inTrigger;
    private ParticleSystem m_part;
    private Animator m_playerAnim;
    public GameObject m_controlledGuard;
    private Animator m_guardAnim;
    private GuardPlayerControl m_guard;
    private float m_turn, m_forward;
    public static float waitTime;
    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] public CinemachineVirtualCamera guardCam;
    [SerializeField] private AudioSource m_mcAudio;
    [SerializeField] private Material mindControlIndMat;
    private Transform[] m_points;
    // Use this for initialization
    void Start () {
        waitTime = 10.0f;
        m_agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        m_part = this.GetComponentInChildren<ParticleSystem>();
        m_playerMoveScript = player.GetComponent<PlayerMovement>();
        m_playerAnim = player.GetComponent<Animator>();
        m_guard = this.GetComponent<GuardPlayerControl>();
        m_guardAnim = this.GetComponent<Animator>();
        m_guardNorm = this.GetComponent<GuardNormalMovement>();
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
        m_turn = Input.GetAxis("Horizontal"); //Setting player input to influence NPC values
        m_forward = Input.GetAxis("Vertical");
        if (control)
        {
            m_playerAnim.SetBool("Controlling", true); //For head animation
            m_controlledGuard = this.gameObject; //Prevents other guards from being affected by this guard being controlled
            m_guard.Move(m_turn, m_forward); // Actual movement of guard
        }
       else if(!control)
        {
            m_playerAnim.SetBool("Controlling", false);
        }
       if(m_inTrigger)
        {
            mindControlIndMat.color = new Color(0.9f,0,0,0.5f); //Changes the colour of the indicator to show if a guard can be controlled
            if (Input.GetButtonDown("Start"))
            {
                print("Start Controlling");
                StartCoroutine(controlling());
            }
        }
       else if (!m_inTrigger)
        {
            mindControlIndMat.color = new Color(1, 1, 1, 0.5f); //Returning colour to normal
        }
    }

    //Detecting if the guard is in the player's trigger
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
    //Setting all guard control elements (camera, control etc.) then returning to normal after the time set as 'waitTime'
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
        m_guardNorm.m_NPCState = GuardNormalMovement.NPCState.PATROL;
        m_guardNorm.m_NavMeshAgent.SetDestination((m_points[m_guardNorm.m_CurrentWaypoint].position));
        m_guardNorm.HandleAnimation();
        m_controlledGuard = null;
    }
    }
