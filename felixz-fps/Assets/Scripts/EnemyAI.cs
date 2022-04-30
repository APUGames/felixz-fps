using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5.0f;
    [SerializeField] float turnSpeed = 5.0f;

    NavMeshAgent nMA;

    float distanceToTarget = Mathf.Infinity;

    bool IsProvoked = false;

    EnemyHealth health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();
        nMA = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.IsDead())
        {
            enabled = false;
            nMA.enabled = false;
        }


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
        FaceTarget();

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
            nMA.velocity = (new Vector3(0,0,0));

        }

        private void FaceTarget()
            {
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime* turnSpeed);

            }

        public void OnDamageTaken()
        {
            IsProvoked = true;
        }

    


    private void OnDrawGizmosSelected()
    {
        //Display the chase range
        Gizmos.color = new Color(1,0,0,1.0f); //Choose color
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    
}
