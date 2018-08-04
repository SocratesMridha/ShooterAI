using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeekFromCover_SMB : StateMachineBehaviour {

    private AIController AI;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reference to AI transform and available patrol points
        AI = GameObject.Find("Enemy").GetComponent<AIController>();

        // Check current Magazine for rounds
        if(AI.ammo != 0f)
        {
            CheckPlayerPosition();
        }
        else
        {
            animator.SetBool("isReloading", true);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(AI.peekPos != null)
        {
            if ((AI.peekPos.position - AI.transform.position).magnitude < 0.5f)
            {
                animator.SetBool("isShooting", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    public void CheckPlayerPosition()
    {
        // Check the distance from the peek points to the player
        float OptionA = Vector3.Magnitude(AI.peekPositions[0].position - AI.target.position);
        float OptionB = Vector3.Magnitude(AI.peekPositions[1].position - AI.target.position);

        // Set closesy of two peek positions as the target for agent destination
        if (OptionA < OptionB)
            AI.peekPos = AI.peekPositions[0];
        else AI.peekPos = AI.peekPositions[1];

        // Set new destination for AI
        AI.agent.destination = AI.peekPos.position;
    }
}
