using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    [SerializeField] float hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");

        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            //Destroy(gameObject, 4.0f);
            Die();
        }
    }

    private void Die()
    {
        if(isDead)
        {
            return;
        }
        
        isDead = true;

        GetComponent<Animator>().SetTrigger("die");
    }
}
