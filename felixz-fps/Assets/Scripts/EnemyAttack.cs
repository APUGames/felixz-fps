using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damageAmount = 25f;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (target == null)
        {
            return;
        }

        target.TakeDamage(damageAmount);
        print("Green Raptor Attacks!");
        target.GetComponent<DisplayDamage>().ShowDamageImpact();
    }

}
