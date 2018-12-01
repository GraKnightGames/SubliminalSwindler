using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RampGuardNormalMovement : MonoBehaviour {
    public enum NPCState { CHASE, PATROL };
    public NPCState m_NPCState;
    public NavMeshAgent m_NavMeshAgent;
    public int m_CurrentWaypoint;
    private bool m_isPlayerNear;
    private Animator m_anim;
    private MindControl m_cntrl;

    [SerializeField] float m_FieldOfView;
    [SerializeField] float m_ThresholdDistance;
    [SerializeField] public Transform[] m_Waypoints;
    [SerializeField] GameObject m_Player;

    // Use this for initialization
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_NPCState = NPCState.PATROL;
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_CurrentWaypoint = 0;
        m_NavMeshAgent.updatePosition = true;
        m_NavMeshAgent.updateRotation = true;
        HandleAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
        m_NavMeshAgent.nextPosition = transform.position;
        switch (m_NPCState)
        {
            case NPCState.CHASE:
                Chase();
                break;
            case NPCState.PATROL:
                Patrol();
                break;
            default:
                break;
        }
        print("Waypoint: " + m_CurrentWaypoint);
        if (m_anim.GetBool("Dying"))
        {
            m_anim.SetFloat("Forward", 0);
            m_anim.SetFloat("Turn", 0);
            m_anim.SetLayerWeight(1, 0);
        }

    }
    void CheckPlayer()
    {
        if (m_NPCState == NPCState.PATROL && m_isPlayerNear && CheckFieldOfView() && CheckOclusion())
        {
            m_NPCState = NPCState.CHASE;
            HandleAnimation();
            return;
        }
        if (!CheckOclusion() && m_NPCState == NPCState.CHASE)
        {
            m_NPCState = NPCState.PATROL;
        }
    }
    void Chase()
    {
        m_NavMeshAgent.SetDestination(m_Player.transform.position);
    }

    bool CheckFieldOfView()
    {
        Vector3 direction = m_Player.transform.position - this.transform.position;
        Vector3 angle = (Quaternion.FromToRotation(transform.forward, direction)).eulerAngles;


        if (angle.y > 180.0f) angle.y = 360.0f - angle.y;
        else if (angle.y < -180.0f) angle.y = angle.y + 360.0f;


        if (angle.y < m_FieldOfView / 2)
        {
            return true;
        }

        return false;
    }

    bool CheckOclusion()
    {
        RaycastHit hit;
        Vector3 direction = m_Player.transform.position - transform.position;
        if (Physics.Raycast(this.transform.position, direction, out hit))
        {
            if (hit.collider.gameObject == m_Player)
            {
                return true;
            }
        }
        return false;
    }

    void Patrol()
    {
        CheckWaypointDistance();
        m_NavMeshAgent.SetDestination(m_Waypoints[m_CurrentWaypoint].position);
    }

    void CheckWaypointDistance()
    {
        if (Vector3.Distance(m_Waypoints[m_CurrentWaypoint].position, this.transform.position) < m_ThresholdDistance)
        {
            m_CurrentWaypoint = (m_CurrentWaypoint + 1) % m_Waypoints.Length;
        }
    }
    private void OnTriggerStay(Collider other) // CheckOclusion?
    {
        if (other.tag == "Player" && CheckFieldOfView())
        {
            m_isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_isPlayerNear = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5.0f);

        Gizmos.color = Color.red;
        Vector3 dir = m_Player.transform.position + transform.position;
        Gizmos.DrawRay(transform.position, dir);
        Vector3 rightDir = Quaternion.AngleAxis(m_FieldOfView / 2, Vector3.up) * transform.forward;
        Vector3 leftDir = Quaternion.AngleAxis(-m_FieldOfView / 2, Vector3.up) * transform.forward;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, rightDir * 5);
        Gizmos.DrawRay(transform.position, leftDir * 5);
    }
    public void HandleAnimation()
    {
        m_NavMeshAgent.nextPosition = transform.position;
        if (m_NPCState == NPCState.CHASE)
        {
            m_anim.SetFloat("Forward", 2);
        }
        else
        {
            m_anim.SetFloat("Forward", 1);
        }
    }
}
       