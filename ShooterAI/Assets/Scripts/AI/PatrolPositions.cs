using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPositions : MonoBehaviour {
    
    public List<Transform> patrolPoints = new List<Transform>();

    public Vector3[] GetClosestPoints(Transform myTrans)
    {
        Vector3[] closestPoints = { Vector3.zero, Vector3.zero };

        for (int i=0; i<patrolPoints.Count-1;i++)
        {
            if(closestPoints[0] != Vector3.zero)
            {
                if ((patrolPoints[i].position - myTrans.position).magnitude < (closestPoints[0] - myTrans.position).magnitude)
                {
                    closestPoints[1] = closestPoints[0];
                    closestPoints[0] = patrolPoints[i].position;
                }
                else if ((patrolPoints[i].position - myTrans.position).magnitude < (closestPoints[1] - myTrans.position).magnitude)
                {
                    closestPoints[1] = patrolPoints[i].position;
                }
            }
            else closestPoints[0] = patrolPoints[i].position;
        }

        return closestPoints;
    }
}
