using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class RunToCover_SMB : StateMachineBehaviour {

    private AIController AI;
    public LayerMask isCoverLayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reference to AI transform and available patrol points
        AI = GameObject.Find("Enemy").GetComponent<AIController>();
        FindNearestCover();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AI.currentCover != null)
        {
            if ((AI.myTrans.position - AI.currentCover.position).magnitude < 0.5f)
            {
                // Set the peek positions of the current cover on AI
                AIController.instance.peekPositions = AI.currentCover.GetComponent<Cover>().GetPeekPositions();
                
                // Transition to peek from cover
                animator.SetBool("isCovered", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isCovered", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    private void FindNearestCover()
    {
        Vector3 closestCover = Vector3.zero;
        foreach(Collider c in Physics.OverlapSphere(AI.myTrans.position, 20f, isCoverLayer))
        {
            if (closestCover != Vector3.zero)
            {
                if ((c.transform.position - AI.myTrans.position).magnitude < (closestCover - AI.myTrans.position).magnitude)
                {
                    closestCover = c.transform.position;
                    AI.currentCover = c.transform;
                }
            }
            else
            {
                closestCover = c.transform.position;
                AI.currentCover = c.transform;
            }
        }

        Debug.Log("Running to Cover");
        AI.agent.destination = closestCover;
    }
}
