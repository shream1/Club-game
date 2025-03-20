using UnityEngine;
using System.Collections;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip bulletSound;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private int maxAmmo = 2;
    [SerializeField] private float reloadTime = 1.5f;

    private AudioSource audioSource;
    private float nextFireTime = 0f;
    private int currentAmmo;
    private bool isReloading = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading) return;

        HandleShooting();

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }
    private void HandleShooting()
    {
        if (currentAmmo <= 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            nextFireTime = Time.time + fireRate;
            ShootShotgun();
        }
    }
    private void ShootShotgun()
    {
        if (currentAmmo > 0)
        {
            float spreadAngle = 10f;
            for (int i = -1; i <= 3; i++)
            {
                float randomOffset = Random.Range(-spreadAngle / 4, spreadAngle / 4);
                Quaternion spreadRotation = Quaternion.Euler(0, 0, i * spreadAngle + randomOffset);
                Vector3 direction = spreadRotation * transform.up;
                InstantiateBullet(firePoint.position, direction);

                //InstantiateBullet(firePoint.position, transform.up * -1f * direction.normalized.y);

            }
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
