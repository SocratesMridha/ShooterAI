using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float health;

    public void DealDamage(float damage)
    {
        this.health -= damage;
        if(this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
