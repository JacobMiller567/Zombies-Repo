using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieMovement : MonoBehaviour
{
  [SerializeField] private Animator animator;
  private GameObject Player;
  private NavMeshAgent Agent;

  [SerializeField] private float maxSpeed;
  [SerializeField] private  float minSpeed;

  //private float updateDestinationInterval = 0.1f;
  //private bool isUpdatingDestination = false; // TEST

  private void Awake()
  {
      Player = GameObject.FindGameObjectWithTag("Player");
      Agent = GetComponent<NavMeshAgent>();
  }

  private void Start()
  {
    Agent.speed = Random.Range(minSpeed, maxSpeed);
    //StartCoroutine(FollowPlayer());
  }

  void Update() // CAUSES: 225+ FPS when looking at 30+ zombies
  {
    Agent.SetDestination(Player.transform.position); 
  }

/*
  void FixedUpdate() // CAUSES: 230+ FPS when looking at 30+ zombies
  {
    Agent.SetDestination(Player.transform.position);      
  }
  */


  /// <summary>
  /// Attack when zombie collides with player
  /// Continue attacking when animation ends
  /// </summary>
  /// 
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
