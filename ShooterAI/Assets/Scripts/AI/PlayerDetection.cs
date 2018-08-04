using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour {

    // Player enters detection: Set State Machine condition
    private void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Player")
        {
            this.GetComponent<Animator>().SetBool("isPatrolling", false);
            this.GetComponent<Animator>().SetBool("playerSpotted", true);


            // Set player detected as the target
            AIController.instance.target = c.transform;
        }
    }

    // Player leaves detection: Patrol
    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            this.GetComponent<Animator>().SetBool("isPatrolling", true);

            // SReset target
            AIController.instance.target = null;
        }
    }
}
