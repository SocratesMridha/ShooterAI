using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_SMB : StateMachineBehaviour
{
    private AIController AI;
    private float timer = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AI = GameObject.Find("Enemy").GetComponent<AIController>();
        Debug.Log("Start Shooting");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AI.ammo != 0f)
            Shoot();
        else
        {
            AI.agent.destination = AI.currentCover.position;
            animator.SetBool("isReloading", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isShooting", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    private void Shoot()
    {
        if (timer >= AI.fireRate)
        {
            // Deal Damage to target
            Health targetHealth = AI.target.GetComponent<Health>();
            if (targetHealth != null)
                targetHealth.DealDamage(AI.damage);

            // Reduce ammunition
            AI.ammo -= 1;

            // reset
            timer = 0;
        }
        else timer += Time.deltaTime;
    }
}
