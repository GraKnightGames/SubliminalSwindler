using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GuardNormalMovement : MonoBehaviour
{
    //Defining Variables and States
    public enum NPCState { CHASE, PATROL };
    public NPCState m_NPCState;
    public NavMeshAgent m_NavMeshAgent;
    public int m_CurrentWaypoint;
    private bool m_isPlayerNear;
    private Animator m_anim;
    private MindControl m_cntrl;
    private bool m_waiting;
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
        if (this.tag == "Guard") //Determining if the guard is a normal type or the one placed on the ramp
        {
            m_NavMeshAgent.updatePosition = false;
            m_NavMeshAgent.updateRotation = true;
        }
        else if(this.name == "RampGuard")
        {
            m_NavMeshAgent.updatePosition = true;
            m_NavMeshAgent.updateRotation = true;
        }
        HandleAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer(); //Checks if the player is within the field of view
        m_NavMeshAgent.nextPosition = transform.position;
        switch (m_NPCState) //Detecting the current state of the NPC
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
        if (m_anim.GetBool("Dying")) //If the guard has been killed, change the weight of the animation layer to allow it to affect more than just in the layer
        {
            m_anim.SetFloat("Forward", 0);
            m_anim.SetFloat("Turn", 0);
            m_anim.SetLayerWeight(1, 0);
        }
        if (m_waiting) //Coroutine for waiting
        {
            StartCoroutine(Wait());
            m_waiting = false;
        }
        else
        {

        }
    }
    void CheckPlayer() //Checking the location of the player
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
        m_NavMeshAgent.SetDestination(m_Player.transform.position);//Setting the agent's destination to the player
        m_anim.SetBool("isFiring", true);
    }
    IEnumerator Wait() //Stop rotation and trigger animation state to go from walking to stopping, then wait and go back to walking and enable rotation, BUT only if patrolling
    {
        if (m_NPCState == NPCState.PATROL)
        {
            m_NavMeshAgent.isStopped = true;
            m_anim.SetBool("Stopping", true);
            yield return new WaitForSeconds(2);
            m_NavMeshAgent.isStopped = false;
            m_anim.SetBool("Stopping", false);
        }
        else
        {

        }
    }
    bool CheckFieldOfView() //Checking field of view
    {
        Vector3 direction = m_Player.transform.position - this.transform.position; //Direction the player is in relation to the NPC
        Vector3 angle = (Quaternion.FromToRotation(transform.forward, direction)).eulerAngles; //Angle the player is in relation to the NPC


        if (angle.y > 180.0f) angle.y = 360.0f - angle.y;
        else if (angle.y < -180.0f) angle.y = angle.y + 360.0f;


        if (angle.y < m_FieldOfView / 2)
        {
            return true; //Player is within the FOV
        }
        else
        {
            return false; //Player is not within the FOV
        }
    }

    bool CheckOclusion() //Checking if there are walls blocking the player
    {
        RaycastHit hit;
        Vector3 direction = m_Player.transform.position - transform.position;
        if (Physics.Raycast(this.transform.position, direction, out hit)) //Casting a ray from the NPC in the direction of the player
        {
            if (hit.collider.gameObject == m_Player)
            {
                return true; //If the ray hits the player, there are no walls blocking the player and guards
            }
        }
            return false;
    }

    void Patrol() //Patrolling behaviour
    {
        CheckWaypointDistance(); //Checking the distance between the NPC and the current waypoint
        m_NavMeshAgent.SetDestination(m_Waypoints[m_CurrentWaypoint].position);
    }

    void CheckWaypointDistance()
    {
            if (Vector3.Distance(m_Waypoints[m_CurrentWaypoint].position, this.transform.position) < m_ThresholdDistance) //If the enemy is on or near (affected by threshold distance var) the waypoint, change the waypoint)
            {
            m_waiting = true; //Stopping and waiting for two seconds at each waypoint
            m_CurrentWaypoint = (m_CurrentWaypoint + 1) % m_Waypoints.Length;
            }
        }
    private void OnTriggerStay(Collider other) // CheckOclusion?
    {
        if (other.tag == "Player" && CheckFieldOfView())
        {
            m_isPlayerNear = true; //If an object with the player tag is within field of view, set variable to true
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_isPlayerNear = false; //Resets to false
        }
    }
    private void OnDrawGizmos() //Draws field of view in scene view for ease of testing
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
    public void HandleAnimation() //If the guard is chasing the player, change the animation to a run animation, if not walk
    {
        m_NavMeshAgent.nextPosition = transform.position;
        if (m_NPCState == NPCState.CHASE)
        {
            m_anim.SetFloat("Forward", 2);
        }
        else if (m_NPCState == NPCState.PATROL)
        {
            m_anim.SetFloat("Forward", 1);
        }
    }
}
        