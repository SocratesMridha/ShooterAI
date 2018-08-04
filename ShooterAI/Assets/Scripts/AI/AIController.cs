using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AIController : MonoBehaviour {

    // Singleton
    public static AIController instance = null;

    private Animator myAnim;

    [Header("Components")]
    public NavMeshAgent agent;
    public Transform myTrans;
    
    [Header("Cover")]
    public Transform[] peekPositions;
    public Transform currentCover;
    public Transform peekPos;

    [Header("Shooting")]
    public Transform target;
    public float damage;
    public float fireRate;
    public float maxAmmo;
    public float ammo;

    [Header("Reload")]
    public float reloadSpeed;

    // Singleton initialization + anim Get Call
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);

        if (!myAnim)
            myAnim = this.GetComponent<Animator>();
    }
}