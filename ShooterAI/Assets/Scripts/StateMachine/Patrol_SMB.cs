using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_SMB : StateMachineBehaviour {

    private AIController AI;
    private PatrolPositions patrolPos;
    private Vector3[] patrolPoints = { Vector3.zero, Vector3.zero };

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reference to AI transform and available patrol points
        AI = GameObject.Find("Enemy").GetComponent<AIController>();
        patrolPos = GameObject.Find("EnemyPatrolPoints").GetComponent<PatrolPositions>();
        Debug.Log("Go Patrol");

        // Returns the two nearest patrol points to AI
        patrolPoints = patrolPos.GetClosestPoints(AI.myTrans);

        // Set to start Patrolling
        AI.agent.destination = patrolPoints[0];
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("isPatrolling") == true)
        {
            // Conditions to PingPong target patrol points: AI within a stopping distance
            if ((AI.myTrans.position - patrolPoints[0]).magnitude < 0.5f)
            {
                AI.agent.destination = patrolPoints[1];
            }
            else if ((AI.myTrans.position - patrolPoints[1]).magnitude < 0.5f)
            {
                AI.agent.destination = patrolPoints[0];
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPatrolling", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
    


}
