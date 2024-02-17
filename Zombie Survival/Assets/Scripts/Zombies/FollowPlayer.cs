using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private GameObject Player;
    private NavMeshAgent Agent;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        Agent.SetDestination(Player.transform.position);
        /*
        if (Agent.pathStatus == NavMeshPathStatus.PathInvalid || Agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
          Agent.speed = .5f;
        }
        */
        
        /*
        if (Agent.isOnOffMeshLink) // Check if this causes lag
        {
            OffMeshLinkData data = Agent.currentOffMeshLinkData;

            //calculate the final point of the link
            Vector3 endPos = data.endPos + Vector3.up * Agent.baseOffset;

            //Move the agent to the end point
            Agent.transform.position = Vector3.MoveTowards(Agent.transform.position, endPos, Agent.speed * Time.deltaTime);

            //when the agent reach the end point you should tell it, and the agent will "exit" the link and work normally after that
            if (Agent.transform.position == endPos)
            {
                Agent.CompleteOffMeshLink();
            }
        }
        
        else // TEST
        {
          Agent.SetDestination(Player.transform.position);
        }
        */
        
    }

    /// <summary>
    /// Attack when zombie collides with player
    /// Continue attacking when animation ends
    /// </summary>
    /// 

/*
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          //  Debug.Log("Initial Hit!");
            animator.SetTrigger("Attack");
            animator.SetBool("AttackEvent", true);
          //  Damage player = collision.gameObject.transform.GetComponent<Damage>();
          //  player?.TakeDamage(25);
        }
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && animator.GetBool("AttackEvent") == false)
        {
          //  Debug.Log("Continous Hit!");
            animator.SetTrigger("Attack");
            animator.SetBool("AttackEvent", true);
        }
    }
*/



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          //  Debug.Log("Initial Hit!");
            animator.SetTrigger("Attack");
            animator.SetBool("AttackEvent", true);
          //  Damage player = collision.gameObject.transform.GetComponent<Damage>();
          //  player?.TakeDamage(25);
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && animator.GetBool("AttackEvent") == false)
        {
          //  Debug.Log("Continous Hit!");
            animator.SetTrigger("Attack");
            animator.SetBool("AttackEvent", true);
        }
    }
    

    public void AttackEnd() // Animation Event
    {
       // Debug.Log("End of Attack");
        animator.SetBool("AttackEvent", false);
    }
}
