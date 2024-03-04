using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject testHole;
    //private AudioSource gunAudio;
    [SerializeField] private bool isShotgun;
    [SerializeField] private bool isSniper; // TEST
    [SerializeField] private bool UnlimitedAmmo = false;
    [SerializeField] private float shotgunPellets = 6;
    [SerializeField] private float shotgunSpreadAngle = 25;

    float timeLastShot;
    private Coroutine reloadCoroutine;
    private bool isActive = false;

    // TEST BELOW //
    public float recoilForce = 0.5f;
    public float recoilRecoverySpeed = 2f;
    public float maxRecoil = 2f;
    private Vector3 initialRotation;
    private Vector3 recoil;
    // END OF TEST //
    private Gun currentGun;
    public Material gunDecal;
    [SerializeField] private Animator animator; // TEST
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

        recoil = Vector3.Lerp(recoil, Vector3.zero, recoilRecoverySpeed * Time.deltaTime);
        transform.localEulerAngles = initialRotation + recoil;


    }

    public void FinishReload() // Animation Event
    {
        finishedReload = true; // TEST
        Debug.Log("TEST");
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
            finishedReload = false; // TEST 
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
       // animator.SetBool("reloading", true);
        animator.SetTrigger("Reloading");
        //yield return new WaitUntil(() => finishedReload == true); // TEST
        yield return new WaitForSeconds(gunData.reloadSpeed);

        if (UnlimitedAmmo)
        {
            int bulletsToRefill = Mathf.Min(gunData.magazineSize, gunData.ammo - gunData.RuntimeAmmo);
            gunData.RuntimeAmmo += bulletsToRefill;
            finishedReload = false; // TEST
        }
        else
        {
            int bulletsUsed = gunData.ammo - gunData.RuntimeAmmo;
            int remainingTotalAmmo = Mathf.Max(0, gunData.magazineSize - bulletsUsed);
            gunData.RuntimeAmmo = Mathf.Min(gunData.ammo, remainingTotalAmmo);
            gunData.RuntimeMagazine = Mathf.Max(0, gunData.RuntimeMagazine - bulletsUsed);
            finishedReload = false; // TEST
        }
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
                float _recoilForce = recoilForce;
                float _recoilRecoverySpeed = recoilRecoverySpeed;

                if (Shooting.Instance.isZooming)
                {
                    _recoilForce *= 0.5f; // Make recoil easier to control
                    _recoilRecoverySpeed *= 0.5f; // Adjust the recoil recovery speed

                    //CrosshairManager.Instance.SetCrosshairScale(0.1f); // TEST
                }

                Vector3 raycastDirection = transform.forward;
                raycastDirection = Quaternion.Euler(recoil) * raycastDirection; 

                if (isShotgun)
                {
                    for (int i = 0; i < shotgunPellets; i++)
                    {
                        Vector3 spreadDirection = CalculateSpreadDirection();

                        if (Physics.Raycast(muzzle.position, spreadDirection, out RaycastHit hitInfo, gunData.maxRange))
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
                            // TEST SPREAD:
                            //Instantiate(testHole, hitInfo.point + new Vector3(hitInfo.normal.x * 0.01f, hitInfo.normal.y * 0.01f, hitInfo.normal.z * 0.01f), Quaternion.LookRotation(-hitInfo.normal));       
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

                    if (Physics.Raycast(muzzle.position, raycastDirection, out RaycastHit hitInfo, gunData.maxRange))
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
                recoil += new Vector3(Random.Range(-_recoilForce, _recoilForce), Random.Range(-_recoilForce, _recoilForce), 0);
                recoil = Vector3.ClampMagnitude(recoil, maxRecoil);
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
    private Vector3 CalculateSpreadDirection()
    {
        Vector3 raycastDirection = transform.forward;
        raycastDirection = Quaternion.Euler(recoil) * raycastDirection;

        // Calculate spread direction with random variation with a cone based around the initial raycast
        float spreadRadius = Mathf.Tan(Mathf.Deg2Rad * shotgunSpreadAngle);
        Vector2 randomSpread = Random.insideUnitCircle * spreadRadius;
        Vector3 spreadDirection = raycastDirection + transform.right * randomSpread.x + transform.up * randomSpread.y;
        return spreadDirection.normalized;
    }              

    public GunData GetGunData()
    {
        return gunData;
    }

    public void RefillAmmo()
    {
        gunData.RuntimeAmmo = gunData.ammo;
        gunData.RuntimeMagazine = gunData.magazineSize;
        AmmoDisplay.instance.UpdateAmmo();
    }

    private void GunShot(){}
}
