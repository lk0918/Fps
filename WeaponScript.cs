using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] float raza = 6f;
    [SerializeField] float damage = 15f;
    [SerializeField] int initialAmmo = 18;
    [SerializeField] int PistolAmmoCapacity = 18;
    [SerializeField] float standartFireRate = 3;
    [SerializeField] public float fireRate = 3;
    [SerializeField] private float nextFireTime = 0f;              
    [SerializeField] ParticleSystem FoculArmei;
    [SerializeField] GameObject HitEffect;
    [SerializeField] Camera FPCamera;

    Animator animator;

    
    [SerializeField] AudioClip fire1;
    [SerializeField] AudioClip reload1;
    [SerializeField] AudioClip reload2;

    [SerializeField] AudioClip hit1;
    [SerializeField] AudioClip hit2;
    [SerializeField] AudioClip hit3;

    [SerializeField] AudioClip bloodhit1;
    [SerializeField] AudioClip bloodhit2;

    bool disableReloadbtn;
    bool isShooting;
    bool isReloading = false;
    bool isMoving;

    int isMovingHash = Animator.StringToHash("moving");




    void Start()
    {
        animator = GetComponent<Animator>();
        
     
       
        

    }
    void Update()
    {

        Shoot();
        ReloadCheck();
        MovingAnimation();
    }


    

   void MovingAnimation()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
           
            animator.SetBool(isMovingHash,true);
            animator.ResetTrigger("idle");

        }
        else
        {
           
            animator.SetBool(isMovingHash ,false);
            animator.SetTrigger("idle");
        }
    }



    void ReloadCheck()
    {
        if (Input.GetButtonDown("Reload") && initialAmmo > PistolAmmoCapacity && !disableReloadbtn)
        {
            AudioSource.PlayClipAtPoint(reload1, transform.position);
            isReloading = true;
            disableReloadbtn = true;
            animator.SetTrigger("reload");
            animator.ResetTrigger("shoot");
            animator.ResetTrigger("idle");
            Invoke(nameof(ReloadAmmoSound), 1);
            Invoke(nameof(ResetAmmo), 2);



        }
      
        
    }
 
    

  void Reload()
    {
        AudioSource.PlayClipAtPoint(reload1,gameObject.transform.position);
        isReloading = true;
        disableReloadbtn = true;
        animator.SetTrigger("reload");
        animator.ResetTrigger("shoot");
        animator.ResetTrigger("idle");
        Invoke(nameof(ReloadAmmoSound), 1);
        Invoke(nameof(ResetAmmo), 2);

    }

    private void Shoot()
    {
        

        if (Input.GetButton("Fire1")  && Time.time >= nextFireTime && !isReloading && !isShooting)
        {
              if(PistolAmmoCapacity <= 1)
                {
                    Reload();
                }

            

            animator.ResetTrigger("idle");
            nextFireTime = Time.time + 1f / fireRate;
            AudioSource.PlayClipAtPoint(fire1, transform.position);
            FoculArmei.Play();
            ProceseazaRaycast();
            animator.SetTrigger("shoot");
            IncrementAmmo();

   
      

        }
            else if (Input.GetButton("Fire2") && Time.time >= nextFireTime && !isReloading && !isShooting)
            {

                     if (PistolAmmoCapacity <= 1)
            {
                Reload();
            }


            fireRate = 5f;
            animator.ResetTrigger("idle");
            nextFireTime = Time.time + 1f / fireRate;
            AudioSource.PlayClipAtPoint(fire1, transform.position);
            FoculArmei.Play();
            ProceseazaRaycast();
            animator.SetTrigger("rapidfire");
            IncrementAmmo();

        }

        else
        {
            isShooting = false;
            fireRate = standartFireRate;
            
        }

    }



  

    void ResetAmmo()
    {   
        PistolAmmoCapacity = initialAmmo;
        isReloading = false;
        disableReloadbtn = false;
    }

   


    void RandomEnemyHitSound(RaycastHit hit)
    {
       
        
        int RNG1 = UnityEngine.Random.Range(1, 2);
        switch (RNG1)
        {
            case 1:
                AudioSource.PlayClipAtPoint(bloodhit1, hit.point);
                break;

            case 2:
                AudioSource.PlayClipAtPoint(bloodhit2, hit.point);
                break;
            default:
                AudioSource.PlayClipAtPoint(bloodhit1, hit.point);
                break;


        }

      
    }



    public void ProceseazaRaycast()
    {

        {
            RaycastHit hit;
            if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, raza))
            {

                CreazaImpactul(hit);
            
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            
                if (target == null) return;
                target.PrimesteDamage(damage);
                RandomEnemyHitSound(hit);
                
               




            }
            else
            {
                return;
            }

        }
    }

    void IncrementAmmo()
    {
        PistolAmmoCapacity--;
    }
    void CreazaImpactul(RaycastHit hit)
    {
        
        GameObject impact = Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        RandomHitSound(hit);
        Destroy(impact, 1);
    }
    

    void ReloadAmmoSound()
    {
        AudioSource.PlayClipAtPoint(reload2,transform.position);
    }

    void RandomHitSound(RaycastHit hit)
    {
     

        int RNG = UnityEngine.Random.Range(1, 3);
        switch (RNG)
        {
            case 1:
                AudioSource.PlayClipAtPoint(hit1, hit.point);
                break;

            case 2:
                AudioSource.PlayClipAtPoint(hit2, hit.point);
                break;

            case 3:
                AudioSource.PlayClipAtPoint(hit3, hit.point);
                break;

            default:
                AudioSource.PlayClipAtPoint(hit1, hit.point);
                break;

        }






    }    

 
   
   
} 


