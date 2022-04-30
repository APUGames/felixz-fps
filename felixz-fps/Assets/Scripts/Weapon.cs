using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fPCamera;

    [SerializeField] float fireDelay = 1.0f;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 20f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitFX;

    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] AudioClip fireSound;
    [SerializeField] TextMeshProUGUI ammoText;

    bool canShoot = false;

    // Update is called once per frame

    private void OnEnable()
    {
        canShoot = true;
    }

    private void Start()
    {
        fireSound = GetComponent<AudioClip>();
    }

    void Update()
    {
        DisplayAmmo();
        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if(ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            ProcessRayCast();
            PlayMuzzleFlash();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(fireDelay);
        canShoot = true;
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }
    private void ProcessRayCast()
    {
        RaycastHit hit;

        if(Physics.Raycast(fPCamera.transform.position, fPCamera.transform.forward, out hit, range))
        {
            print("I hit this: " + hit.transform.name);

            createHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if(target == null)
            {
                return;
            }

            target.TakeDamage(damage);

            
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
