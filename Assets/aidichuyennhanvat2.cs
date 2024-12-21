using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.AI;
using UnityEngine;

public class aidichuyennhanvat2 : StateMachineBehaviour
{
    float Timer;
    float chaserange =15 ;
     NavMeshAgent agent;
     List<Transform> waypoints=new List<Transform>();
     Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer=0;
        Transform waypointobject=GameObject.FindGameObjectWithTag("waypoint").transform;
        foreach(Transform t in waypointobject)
        {
            waypoints.Add(t);
            
         
        }
        agent=animator.GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[0].position);
        player =GameObject.FindWithTag("Player").transform;
          
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent.remainingDistance<=agent.stoppingDistance)
        {
            agent.SetDestination(waypoints[Random.Range(0,waypoints.Count)].position);
        }
        Timer+=Time.deltaTime;
        if(Timer>15)
        {
            animator.SetBool("isrun",false);
        }
        float distance =Vector3.Distance(animator.transform.position,player.position);
        if(distance<7)
        {
            animator.SetBool("istancong",true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    //    
         agent.ResetPath();
         agent.SetDestination(agent.transform.position);
      
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    //    // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    //    // Implement code that sets up animation IK (inverse kinematics)
    }
}
