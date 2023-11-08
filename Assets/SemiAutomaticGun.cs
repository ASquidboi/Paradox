using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SemiAutomaticGun : MonoBehaviour
{
    //Various gun info
    public float damage = 40f;
    public float range = 100f;
    public float fireRate = 5f;
    //Camera for raycasting
    public Camera fpsCam;
    public CinemachineVirtualCamera fovCam;
    //Impact effects & muzzle flash
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactForce = 30f;
    //Weapon timing
    public float nextTimeToFire = 0f;
    //Animation stuff
    public Animator animator;
    public AudioSource firingSound;
    public float ammo = 12f;
    public float magsize = 12f;
    public float FOV = 35f;
    public float normalFOV = 45f;
    public bool ADS = false;

    //Ammo

    //public float ammo = 25f;
    //public float magsize = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ammo > 0)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
        if(Input.GetButtonDown("Reload"))
        {
            Reload();
        }
        void Shoot() 
        {
            if (ADS == false) {
                animator.SetTrigger("Shoot");
            } else { //true
                Debug.Log("ADSShoot");
                animator.SetBool("ADS", !ADS);
                animator.SetTrigger("ADSShoot");
                animator.SetBool("ADS", !ADS);
            }
            
            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
                if(target != null) 
                {
                    target.TakeDamage(damage);
                }
                fracture glass = hit.transform.GetComponent<fracture>();
                if(glass != null) 
                {
                    glass.Shatter();
                }
                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
                ammo -= 1f;
            }
            firingSound.Play();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            ADS = !ADS;
            animator.SetBool("ADS", ADS);
            if (ADS){
                StartCoroutine(OnScoped());
            } else {
                OnUnscoped();
            }
        }

        IEnumerator OnScoped()
        {
            yield return new WaitForSeconds(0.15f);

            // normalFOV = fpsCam.fieldOfView;
            // fovCam. = FOV;
            fovCam.m_Lens.FieldOfView = FOV;
            
        }
    }
    void OnUnscoped()
        {
            //fovCam. = normalFOV;
            fovCam.m_Lens.FieldOfView = normalFOV;
        }
    void Reload()
        {
            animator.SetTrigger("Reload");
            ammo = magsize;
        }
}
