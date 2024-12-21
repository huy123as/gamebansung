using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class patrolbehavior1 : StateMachineBehaviour
{
    float timer;
    List<Transform> Waypoints = new List<Transform>();
    NavMeshAgent agent;

    // Tầm nhìn hoặc khoảng cách "nhìn thấy" người chơi (nếu cần)
    float chaseRange = 10f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Đặt lại timer
        timer = 0;

        // Gán NavMeshAgent
        agent = animator.GetComponent<NavMeshAgent>();

        // Lấy danh sách các waypoint
        Transform wayPointsObject = GameObject.FindGameObjectWithTag("waypoint").transform;
        foreach (Transform t in wayPointsObject)
        {
            Waypoints.Add(t);
        }

        // Kiểm tra Waypoints tồn tại
        if (Waypoints.Count > 0)
        {
            // Di chuyển tới waypoint đầu tiên
            agent.SetDestination(Waypoints[0].position);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Kiểm tra nếu AI đã đến gần waypoint hiện tại
        if (agent.remainingDistance <= agent.stoppingDistance && Waypoints.Count > 0)
        {
            // Chọn waypoint ngẫu nhiên
            agent.SetDestination(Waypoints[Random.Range(0, Waypoints.Count)].position);
        }

        // Tăng timer để thoát khỏi trạng thái nếu đủ thời gian
        timer += Time.deltaTime;
        if (timer > 10)
        {
            animator.SetBool("ipatroling1", false); // Thoát trạng thái tuần tra
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Dừng di chuyển khi rời trạng thái
        if (agent != null)
        {
            agent.SetDestination(agent.transform.position);
        }
    }
}
