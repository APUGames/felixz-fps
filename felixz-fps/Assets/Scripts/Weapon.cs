using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 20f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitFX;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        ProcessRayCast();
        PlayMuzzleFlash();
    }
    private void ProcessRayCast()
    {
        RaycastHit hit;

        if(Physics.Raycast(fPCamera.transform.position, fPCamera.transform.forward, out hit, range))
        {
            print("I hit this: " + hit.transform.name);

            createHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            //Crashes whenever I shoot the floor because there's no health/damage for the floor
            //target.TakeDamage(damage);

            
        }

        else
        {
                return;
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void createHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitFX.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1.0f);
    }
}
