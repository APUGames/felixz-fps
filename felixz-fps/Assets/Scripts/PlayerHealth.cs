using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{  
    [SerializeField] float hitPoints = 101f;

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            print ("DED");
            GetComponent<DeathHandler>().ProcessDeath();
        }
        
    }
}
