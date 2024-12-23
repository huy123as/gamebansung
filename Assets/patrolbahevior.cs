using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class patrolbahevior : StateMachineBehaviour
{
        float timer;
        List<Transform> Waypoints = new List<Transform>();
        NavMeshAgent agent;
        Transform player;
        float chaseRange = 10;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer=0;
        Transform wayPointsObject = GameObject.FindGameObjectWithTag("waypoint").transform;
        foreach (Transform t in wayPointsObject)
        {
            Waypoints.Add(t);
        }
            
        agent = animator.GetComponent<NavMeshAgent>();
        agent.SetDestination(Waypoints[1].position);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(Waypoints[Random.Range(0, Waypoints.Count)].position);

        timer += Time.deltaTime;
        if (timer > 10)
            animator.SetBool("ispatrolling", false);
            
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < chaseRange)
        {
            animator.SetBool("ischasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
