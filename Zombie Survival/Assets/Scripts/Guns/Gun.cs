using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] private ParticleSystem muzzleFlash;

    //[SerializeField] private LayerMask headLayerMask; // TEST
   // [SerializeField] private LayerMask bodyLayerMask; // TEST
    [SerializeField] private GameObject bulletHole;
    //private AudioSource gunAudio;
    [SerializeField] private bool isShotgun;
    [SerializeField] private bool UnlimitedAmmo = false;
    float timeLastShot;
    private Coroutine reloadCoroutine;
    private bool isActive = false;

    [SerializeField] private float shotgunPellets = 6;
    [SerializeField] private float shotgunSpreadAngle = 25;

    // TEST BELOW //
   // [SerializeField] private Recoil gunRecoil;
    
    public float recoilForce = 0.5f;
    public float recoilRecoverySpeed = 2f;
    public float maxRecoil = 2f;
    private Vector3 initialRotation;
    private Vector3 recoil;
    //[SerializeField] private Recoil recoilComponent;
    
    // END OF TEST //
    
    void OnEnable()
    {
        isActive = true;
        gunData.reloading = false;
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
       // TEST: 
        //recoil = Vector3.Lerp(recoil, Vector3.zero, recoilRecoverySpeed * Time.deltaTime);
        //transform.localEulerAngles = initialRotation + recoil;

    }


    private void Update()
    {
        timeLastShot += Time.deltaTime;
        Debug.DrawRay(muzzle.position, muzzle.forward);

        //TEST
        recoil = Vector3.Lerp(recoil, Vector3.zero, recoilRecoverySpeed * Time.deltaTime);
        transform.localEulerAngles = initialRotation + recoil;


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
        yield return new WaitForSeconds(gunData.reloadSpeed);

        if (UnlimitedAmmo)
        {
            int bulletsToRefill = Mathf.Min(gunData.magazineSize, gunData.ammo - gunData.RuntimeAmmo);
            gunData.RuntimeAmmo += bulletsToRefill;

        }
        else
        {
            int bulletsUsed = gunData.ammo - gunData.RuntimeAmmo;
            int remainingTotalAmmo = Mathf.Max(0, gunData.magazineSize - bulletsUsed);
            // Refill the chamber with remaining total ammo, up to the maximum magazine size
            gunData.RuntimeAmmo = Mathf.Min(gunData.ammo, remainingTotalAmmo);
            gunData.RuntimeMagazine = Mathf.Max(0, gunData.RuntimeMagazine - bulletsUsed);
        }
        AmmoDisplay.instance.UpdateAmmo();
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeLastShot > 1f / (gunData.fireRate / 60f); 

    public void Shoot()
    {
        if (gunData.RuntimeAmmo > 0)
        {
            if (CanShoot())
            {
                float _recoilForce = recoilForce;
                float _recoilRecoverySpeed = recoilRecoverySpeed;

                if (Shooting.Instance.isZooming)
                {
                    // Apply reduced recoil for zoomed-in state
                    _recoilForce *= 0.5f; // Adjust the recoil force to make it easier to control
                    _recoilRecoverySpeed *= 0.5f; // Adjust the recoil recovery speed accordingly
                }
                //TEST
                //gunRecoil.ApplyRecoil(); // TEST 
                recoil += new Vector3(Random.Range(-_recoilForce, _recoilForce), Random.Range(-_recoilForce, _recoilForce), 0);
                recoil = Vector3.ClampMagnitude(recoil, maxRecoil);
                if (isShotgun)
                {
                    for (int i = 0; i < shotgunPellets; i++)
                    {
                        Vector3 spreadDirection = CalculateSpreadDirection();

                        if (Physics.Raycast(muzzle.position, spreadDirection, out RaycastHit hitInfo, gunData.maxRange))
                        {
                            Damage damagable = hitInfo.transform.GetComponent<Damage>();
                            damagable?.TakeDamage(gunData.RuntimeDamage);

                            if (muzzleFlash.isPlaying)
                            { 
                                muzzleFlash.Stop();
                            }        
                            muzzleFlash.Play();
                        }
                    }
                    gunData.RuntimeAmmo--;
                    AmmoDisplay.instance.UpdateAmmo();
                    timeLastShot = 0;
                    GunShot();
                }
                else
                {
                    Vector3 raycastDirection = transform.forward;
                    raycastDirection = Quaternion.Euler(recoil) * raycastDirection;
                    if (Physics.Raycast(muzzle.position, raycastDirection, out RaycastHit hitInfo, gunData.maxRange))
                    {                  
                        Damage damagable = hitInfo.transform.GetComponent<Damage>();
                        damagable?.TakeDamage(gunData.RuntimeDamage);

                        if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                        {
                            Instantiate(bulletHole, hitInfo.point + new Vector3(hitInfo.normal.x * 0.01f, hitInfo.normal.y * 0.01f, hitInfo.normal.z * 0.01f), Quaternion.LookRotation(-hitInfo.normal));
                        }
                    }
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
    }
    private Vector3 CalculateSpreadDirection()
    {
        Vector2 randomSpread = Random.insideUnitCircle * shotgunSpreadAngle;
        Vector3 spreadDirection = transform.forward + transform.right * randomSpread.x + transform.up * randomSpread.y;
        return spreadDirection.normalized;
    }
                /*
                    // gunAudio.Play();
                    //if (((1 << hitInfo.transform.gameObject.layer) & headLayerMask) != 0)
                    if (hitInfo.transform.gameObject.layer == headLayerMask)
                    {
                        Debug.Log("HEAD SHOT!");
                        Damage damagable = hitInfo.transform.GetComponent<Damage>();
                        damagable?.TakeDamage(gunData.RuntimeDamage);
                    }

                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                    {
                        Instantiate(bulletHole, hitInfo.point + new Vector3(hitInfo.normal.x * 0.01f, hitInfo.normal.y * 0.01f, hitInfo.normal.z * 0.01f), Quaternion.LookRotation(-hitInfo.normal));
                    }

                    else if (((1 << hitInfo.transform.gameObject.layer) & bodyLayerMask) != 0)
                    {
                        Damage damagable = hitInfo.transform.GetComponent<Damage>();
                        damagable?.TakeDamage(gunData.RuntimeDamage);
                        // Perform actions specific to hitting the zombie's body
                    }
                    else
                    {
                       // Damage damagable = hitInfo.transform.GetComponent<Damage>();
                       // damagable?.TakeDamage(gunData.RuntimeDamage);
                    }
                */

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
