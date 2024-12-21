using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public GameObject impactEffect;

    // This method is called when the projectile collides with another object
    private void OnCollisionEnter(Collision collision) 
    {
        
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);

    
        Destroy(impact, 2);
 
        Destroy(gameObject);
    }
}
