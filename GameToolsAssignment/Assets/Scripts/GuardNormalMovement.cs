using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GuardNormalMovement : MonoBehaviour
{
    private enum NPCState { CHASE, PATROL };
    private NPCState m_NPCState;
    private NavMeshAgent m_NavMeshAgent;
    private int m_CurrentWaypoint;
    private bool m_isPlayerNear;
    private Animator m_anim;

    [SerializeField] float m_FieldOfView;
    [SerializeField] float m_ThresholdDistance;
    [SerializeField] private Transform[] m_Waypoints;
    [SerializeField] GameObject m_Player;

    // Use this for initialization
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_NPCState = NPCState.PATROL;
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_CurrentWaypoint = 0;
        m_NavMeshAgent.updatePosition = false;
        m_NavMeshAgent.updateRotation = true;
        HandleAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
        if(m_isPlayerNear)
        {
            Debug.Log("Near");
        }
        if(CheckFOV() == true)
        {
            Debug.Log("FOV");
        }
        if(CheckOclusion() == true)
        {
            Debug.Log("Occ");
        }
        Debug.Log(m_NPCState);
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
    }
    void CheckPlayer()
    {
        if (m_NPCState == NPCState.PATROL && m_isPlayerNear && CheckFOV() && CheckOclusion())
        {
            Debug.Log("Change");
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
        Debug.Log("Chasing");
        m_NavMeshAgent.SetDestination(m_Player.transform.position);
    }

    bool CheckFOV()
    {
        Vector3 direction = m_Player.transform.position - this.transform.position;
        Vector3 angle = (Quaternion.FromToRotation(transform.forward, direction)).eulerAngles;


        if (angle.y > 180.0f)
            angle.y = 360.0f - angle.y;
        else if (angle.y < -180.0f)
            angle.y = angle.y + 360.0f;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (CheckFOV())
            {
                m_isPlayerNear = true;
            }
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
        Vector3 direction = m_Player.transform.position - transform.position;
        Gizmos.DrawRay(transform.position, dir);
        Vector3 rightDir = Quaternion.AngleAxis(m_FieldOfView / 2, Vector3.up) * transform.forward;
        Vector3 leftDir = Quaternion.AngleAxis(-m_FieldOfView / 2, Vector3.up) * transform.forward;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, rightDir * 5);
        Gizmos.DrawRay(transform.position, leftDir * 5);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, direction);
    }
    private void HandleAnimation()
    {
        m_NavMeshAgent.nextPosition = transform.position;
        if (m_NPCState == NPCState.CHASE)
        {
            m_anim.SetFloat("Forward", 2);
            m_anim.SetBool("isFiring", true);
        }
        else
        {
            m_anim.SetFloat("Forward", 1);
        }
    }
}
        //if (m_guardAnim.GetBool("Dying"))
        //{
          //  m_guardAnim.SetFloat("Forward", 0);
          //  m_guardAnim.SetFloat("Turn", 0);
          //  m_guardAnim.SetLayerWeight(1, 0);
       // }
