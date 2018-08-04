using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload_SMB : StateMachineBehaviour
{
    private AIController AI;
    private float timer = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reference to AI transform and available patrol points
        AI = GameObject.Find("Enemy").GetComponent<AIController>();
        Debug.Log("No Ammo, Reloading");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AI.ammo != AI.maxAmmo)
            Reload();
        else
        {
            animator.SetBool("isReloading", false);
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

    private void Reload()
    {
        if(timer >= AI.reloadSpeed)
        {
            // reload magazine
            AI.ammo = AI.maxAmmo;
            Debug.Log("Reloaded");

            // reset
            timer = 0;
        }
        else timer += Time.deltaTime;
    }
}
