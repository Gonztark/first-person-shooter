using System.Collections;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public GameObject muzzleEffect;
    public float bulletVelocity = 30f;
    public float bulletLifetime = 3f;
    public float shootingDelay = 0.5f;
    public float spreadIntensity = 0.1f;
    public int bulletsPerBurst = 3;

    private bool isShooting = false;
    private bool readyToShoot = true;
    private int burstBulletsLeft;
    private float timeSinceLastShot = 0f;

    public enum ShootingMode { Single, Burst, Auto }
    public ShootingMode currentShootingMode;

    private Animator animator;


    //Recargar
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;




    private void Awake()
    {
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;
    }

    void Update()
    {


        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
        {
            Reload();
        }

        //Recargar de forma automatica

        if (readyToShoot && isShooting == false && isReloading == false && bulletsLeft <= 0)
        {
            Reload();
        }


        HandleInput();
        if (isShooting && readyToShoot && bulletsLeft > 0 && !isReloading)
        {
            Shoot();
        }

        if (AmmoManager.Instance.ammoDisplay != null)
        {
            AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft/bulletsPerBurst}/{magazineSize/bulletsPerBurst}";
        }


    }

    private void HandleInput()
    {
        switch (currentShootingMode)
        {
            case ShootingMode.Auto:
                isShooting = Input.GetKey(KeyCode.Mouse0);
                break;
            case ShootingMode.Single:
            case ShootingMode.Burst:
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
                break;
        }
    }

    private void Shoot()
    {
        if (timeSinceLastShot < shootingDelay)
        {
            return; // Exit if within the delay period
        }

        timeSinceLastShot = 0f;
        if (currentShootingMode == ShootingMode.Burst)
        {
            burstBulletsLeft--;
            if (burstBulletsLeft <= 0)
            {
                readyToShoot = false;
                burstBulletsLeft = bulletsPerBurst;
                Invoke("ResetShot", shootingDelay);
                return;
            }
        }
        else
        {
            readyToShoot = false;
            Invoke("ResetShot", shootingDelay);
        }

        // Play muzzle effect and fire bullet
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");
        SoundManager.Instance.shootingSound9M.Play();
        FireBullet();
    }

    private void FireBullet()
    {
        bulletsLeft--;
        Vector3 shootingDirection = CalculateDirectionAndSpread();
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.LookRotation(shootingDirection));
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        Destroy(bullet, bulletLifetime);
    }

    private void Reload()
    {
        SoundManager.Instance.reloadingSound9M.Play();
        animator.SetTrigger("RELOAD");
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint = ray.GetPoint(100);

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }

        Vector3 directionWithoutSpread = targetPoint - bulletSpawn.position;
        Vector3 spread = new Vector3(
            Random.Range(-spreadIntensity, spreadIntensity),
            Random.Range(-spreadIntensity, spreadIntensity),
            Random.Range(-spreadIntensity, spreadIntensity)
        );

        return directionWithoutSpread.normalized + spread;
    }

    private void FixedUpdate()
    {
        timeSinceLastShot += Time.fixedDeltaTime;
    }
}
