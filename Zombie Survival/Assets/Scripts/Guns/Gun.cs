using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource gunShootSound;
    [SerializeField] private AudioSource gunReloadSound;
    [SerializeField] private bool isShotgun;
    [SerializeField] private bool isSniper;
    [SerializeField] private bool UnlimitedAmmo = false;
    [SerializeField] private bool usesBurstFireRate = false; // Shoots in a burst fire-rate
    [SerializeField] private float shotgunPellets = 6; // Shotgun use only
    [SerializeField] private float shotgunSpreadAngle = 25; // Shotgun use only
    [SerializeField] private Recoil gunRecoil; 

    float timeLastShot;
    private Coroutine reloadCoroutine;
    private bool isActive = false;
    private bool isBurstFiring = false; // TEST
    private int burstCount = 3; // TEST
    private Gun currentGun;
    public Material gunDecal;
    [SerializeField] private Animator animator;
    private bool finishedReload;
    
    void OnEnable()
    {
        isActive = true;
        gunData.reloading = false;
        CrosshairManager.Instance.ChangeCrosshair(gunData.name); // Set guns crosshair
        GunSway.Instance.weaponTransform = PlayerInventory.instance.activeGuns[PlayerInventory.instance.GetIndex()].transform; // Apply weapon sway to gun
        currentGun = this;
    }
    void OnDisable()
    {
        isActive = false;
        animator.Rebind();
        animator.SetBool("reloading", false);
        //animator.SetTrigger("Cancel");
    }

    private void Start()
    {
        Shooting.inputShooting += Shoot;
        Shooting.inputReloading += StartReload;
       //gunAudio = GetComponent<AudioSource>();
    }


    private void Update()
    {
        timeLastShot += Time.deltaTime;
        Debug.DrawRay(muzzle.position, muzzle.forward);
    }

    public void FinishReload() // Animation Event
    {
        //animator.SetTrigger("EndReload");
        animator.SetBool("reloading", false);
        finishedReload = true; // TEST
    }

    public void StartReload()
    {
        if (!gunData.reloading && isActive)
        {
            reloadCoroutine = StartCoroutine(Reload());
        }
    }
    public void StopReload()
    {   
        if (reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
            gunData.reloading = false;
            finishedReload = false; 
        }  
    }
    private IEnumerator Reload()
    {
        if (gunData.RuntimeMagazine == 0) 
        {
            gunData.reloading = false;
            yield break;
        }

        gunData.reloading = true;
        animator.SetBool("reloading", true);
        yield return new WaitUntil(() => finishedReload);
        if (UnlimitedAmmo)
        {   
            if (gunData.RuntimeUpgraded)
            {
                int bulletsToRefill = Mathf.Min(gunData.upgradedMagazineSize, gunData.upgradedAmmo - gunData.RuntimeAmmo);
                gunData.RuntimeAmmo += bulletsToRefill;
            }
            else
            {
                int bulletsToRefill = Mathf.Min(gunData.magazineSize, gunData.ammo - gunData.RuntimeAmmo);
                gunData.RuntimeAmmo += bulletsToRefill;
            }
        }
        else
        {
            if (gunData.RuntimeUpgraded)
            {
                int bulletsUsed = gunData.upgradedAmmo - gunData.RuntimeAmmo;
                int remainingTotalAmmo = Mathf.Max(0, gunData.upgradedMagazineSize - bulletsUsed);
                gunData.RuntimeAmmo = Mathf.Min(gunData.upgradedAmmo, remainingTotalAmmo);
                gunData.RuntimeMagazine = Mathf.Max(0, gunData.RuntimeMagazine - bulletsUsed);
            }
            else
            {
                int bulletsUsed = gunData.ammo - gunData.RuntimeAmmo;
                int remainingTotalAmmo = Mathf.Max(0, gunData.magazineSize - bulletsUsed);
                gunData.RuntimeAmmo = Mathf.Min(gunData.ammo, remainingTotalAmmo);
                gunData.RuntimeMagazine = Mathf.Max(0, gunData.RuntimeMagazine - bulletsUsed);
            }
        }
        finishedReload = false;
        AmmoDisplay.instance.UpdateAmmo();
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeLastShot > 1f / (gunData.fireRate / 60f); 

    public void Shoot()
    {
        if (gunData.RuntimeAmmo > 0)
        {
            if (CanShoot() && currentGun.isActiveAndEnabled)
            {
                gunRecoil.ApplyRecoil();
                
                /*
                if (Shooting.Instance.isZooming)
                {
                    //CrosshairManager.Instance.SetCrosshairScale(0.1f); // TEST
                }
                */
                if (usesBurstFireRate && !isBurstFiring)
                {
                    StartCoroutine(BurstFire());
                }


                Vector3 raycastDirection = transform.forward;
               // Vector3 raycastDirection = muzzle.forward;
                //raycastDirection = Quaternion.Euler(recoil) * raycastDirection; 

                if (isShotgun)
                {
                    for (int i = 0; i < shotgunPellets; i++)
                    {
                        Vector3 spreadDirection = CalculateSpreadDirection();

                        if (Physics.Raycast(muzzle.position, spreadDirection, out RaycastHit hitInfo, gunData.RuntimeMaxRange))
                        {
                            if (hitInfo.collider.transform.CompareTag("Head") || hitInfo.collider.CompareTag("Head")) 
                            {   
                                Damage damagable = hitInfo.transform.GetComponent<Damage>();
                                damagable?.TakeDamage(Mathf.RoundToInt(gunData.RuntimeDamage * 1.5f)); // 50% more damage
                            }
                            else
                            {
                                Damage damagable = hitInfo.transform.GetComponent<Damage>();
                                damagable?.TakeDamage(gunData.RuntimeDamage);
                            }

                            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                            {
                                GameObject bulletHole = BulletHolePool.Instance.GetBulletHole();

                                Vector3 offset = new Vector3(hitInfo.normal.x * 0.01f, hitInfo.normal.y * 0.01f, hitInfo.normal.z * 0.01f);
                                bulletHole.transform.position = hitInfo.point + offset;
                            
                                bulletHole.transform.rotation = Quaternion.LookRotation(-hitInfo.normal);

                                bulletHole.SetActive(true);
                            }
                        }
                    }
                }
                else
                {

                    if (Physics.Raycast(muzzle.position, raycastDirection, out RaycastHit hitInfo, gunData.RuntimeMaxRange))
                    {
                        if (hitInfo.collider.transform.CompareTag("Head") || hitInfo.collider.CompareTag("Head")) 
                        {   
                            Damage damagable = hitInfo.transform.GetComponent<Damage>();
                            damagable?.TakeDamage(Mathf.RoundToInt(gunData.RuntimeDamage * 1.5f)); // 50% more damage
                        }
                        else
                        {
                            Damage damagable = hitInfo.transform.GetComponent<Damage>();
                            damagable?.TakeDamage(gunData.RuntimeDamage);
                        }

                        if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                        {
                            GameObject bulletHole = BulletHolePool.Instance.GetBulletHole();

                            Vector3 offset = new Vector3(hitInfo.normal.x * 0.01f, hitInfo.normal.y * 0.01f, hitInfo.normal.z * 0.01f);
                            bulletHole.transform.position = hitInfo.point + offset;
                            
                            bulletHole.transform.rotation = Quaternion.LookRotation(-hitInfo.normal);

                            bulletHole.SetActive(true);
                        }
                    }
                }
                // gunAudio.Play();
                gunShootSound.Play();
                if (muzzleFlash.isPlaying)
                { 
                    muzzleFlash.Stop();
                }   
                muzzleFlash.Play();
                gunData.RuntimeAmmo--;
                AmmoDisplay.instance.UpdateAmmo();
                timeLastShot = 0;
                GunShot();                      
            }
        }
    }
    private Vector3 CalculateSpreadDirection() // Used with Shotguns
    {
        Vector3 raycastDirection = transform.forward;
       // Vector3 raycastDirection = muzzle.forward;
        float spreadAngle = shotgunSpreadAngle;

        // Reduce spread angle if zooming
        if (Shooting.Instance.isZooming)
        {
            spreadAngle *= 0.65f; 
        }

        // Calculate spread direction with random variation in a cone shape
        float spreadRadius = Mathf.Tan(Mathf.Deg2Rad * spreadAngle);
        Vector2 randomSpread = Random.insideUnitCircle * spreadRadius;
        Vector3 spreadDirection = raycastDirection + transform.right * randomSpread.x + transform.up * randomSpread.y;
        return spreadDirection.normalized;
    }

    private IEnumerator BurstFire() // TEST
    {
        isBurstFiring = true;
        while (burstCount < 3)//gunData.burstCount) // Control burst fire count
        {
            Shoot();
            burstCount++;
            //yield return new WaitForSeconds(1f / (gunData.burstRate / 60f)); // Delay between burst shots
            yield return new WaitForSeconds(1f / (150 / 60f)); 
        }
        burstCount = 0; 
        isBurstFiring = false;
    }
              

    public GunData GetGunData()
    {
        return gunData;
    }

    public void RefillAmmo()
    {
        if (gunData.RuntimeUpgraded)
        {
            gunData.RuntimeAmmo = gunData.upgradedAmmo;
            gunData.RuntimeMagazine = gunData.upgradedMagazineSize;
        }
        else
        {
            gunData.RuntimeAmmo = gunData.ammo;
            gunData.RuntimeMagazine = gunData.magazineSize;
        }
        AmmoDisplay.instance.UpdateAmmo();
    }

    private void GunShot(){}
}
