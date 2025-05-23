using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float orbitRadius = 4.4f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip bulletSound;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private float reloadTime = 1.5f;

    private AudioSource audioSource;
    private float nextFireTime = 0f;
    private int currentAmmo;
    private bool isReloading = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (isReloading) return;

        RotateWeaponWithMouse();
        HandleShooting();

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
        
        
    }


    Vector3 direction;
    public float distanceFromPlayer;

 
    private void RotateWeaponWithMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - player.position).normalized;

       
        transform.position = Vector3.Lerp(transform.position, player.position + direction * orbitRadius, Time.deltaTime * 10f);

     
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

       
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 15f);

        Vector2 offset = direction.normalized * distanceFromPlayer;
        Vector2 targetPosition = (Vector2)player.position + offset;
        // Set the position of the rotating object
            transform.position = targetPosition;
        if (direction.x > 0) 
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); 
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 15f);

    }


    private void HandleShooting()
    {
        if (currentAmmo <= 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            nextFireTime = Time.time + fireRate;
            ShootPistol();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ShootShotgun();
        }
        else if (Input.GetMouseButton(0) && Time.time >= nextFireTime )
        {
            nextFireTime = Time.time + fireRate;
            ShootAutoGun();
        }
    }

  
    private void ShootPistol()
    {
        if (currentAmmo > 0)
        {

            //InstantiateBullet(firePoint.position, new Vector2 (transform.position.x - transform.position.z,transform.position.y).normalized);
            InstantiateBullet(firePoint.position, transform.up);

          // InstantiateBullet(firePoint.position, transform.right);

            PlayShootSound();
            currentAmmo--;
        }
    }

    
    private void ShootShotgun()
    {
        if (currentAmmo >= 3)
        {
            float spreadAngle = 10f;
            for (int i = -1; i <= 1; i++)
            {
                float randomOffset = Random.Range(-spreadAngle / 2, spreadAngle / 2);
                Quaternion spreadRotation = Quaternion.Euler(0, 0, i * spreadAngle + randomOffset);
                Vector3 direction = spreadRotation * transform.up;
                InstantiateBullet(firePoint.position, direction);

                //InstantiateBullet(firePoint.position, transform.up * -1f * direction.normalized.y);

            }
            PlayShootSound();
            currentAmmo -= 3;
        }
    }


    private void ShootAutoGun()
    {
        if (currentAmmo > 0)
        {

            // InstantiateBullet(firePoint.position, transform.forward);
            InstantiateBullet(firePoint.position, transform.up);

            //InstantiateBullet(firePoint.position, transform.right);

            PlayShootSound();
            currentAmmo--;
        }
    }

   
    private void InstantiateBullet(Vector3 position, Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

   
    private void PlayShootSound()
    {
        if (bulletSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(bulletSound);
        }
    }

   
    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
