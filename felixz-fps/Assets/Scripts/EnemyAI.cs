using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5.0f;

    NavMeshAgent nMA;

    float distanceToTarget = Mathf.Infinity;

    bool IsProvoked = false;

    // Start is called before the first frame update
    void Start()
    {
        nMA = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(IsProvoked)
        {
            EngageTarget();
        }

        else if(distanceToTarget <= chaseRange)
        {
            
            IsProvoked = true;
        }
    }

    private void EngageTarget()
    {
        if(distanceToTarget>=nMA.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget<=nMA.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
        {
            GetComponent<Animator>().SetBool("attack", false);
            GetComponent<Animator>().SetTrigger("run");
            nMA.SetDestination(target.position);
        }

        private void AttackTarget()
        {
            GetComponent<Animator>().SetBool("attack", true);
            //temp for now
            print(name + " ATTACKS " + target.name);

        }
    


    private void OnDrawGizmosSelected()
    {
        //Display the chase range
        Gizmos.color = new Color(1,0,0,1.0f); //Choose color
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    
}
