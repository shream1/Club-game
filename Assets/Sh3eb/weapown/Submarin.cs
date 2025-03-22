using UnityEngine;
using System.Collections;
using Photon.Pun;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;

public class Submarin : MonoBehaviourPun
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip bulletSound;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private int maxAmmo = 30;
    [SerializeField] private float reloadTime = 1.5f;

    private AudioSource audioSource;
    private float nextFireTime = 0f;
    private int currentAmmo;
    private bool isReloading = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<Submarin>());
        }

        audioSource = GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (isReloading) return;

            HandleShooting();

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
            }
        }
    }

    private void HandleShooting()
    {
        if (photonView.IsMine)
        {
            if (currentAmmo <= 0) return;
            else if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                ShootAutoGun();
            }
        }
    }
    private void ShootAutoGun()
    {
        if (photonView.IsMine)
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
    }

    private void InstantiateBullet(Vector3 position, Vector3 direction)
    {
        if (photonView.IsMine)
        {
            string bullName = bulletPrefab.name;

            GameObject bullet = PhotonNetwork.Instantiate("bunny_hit", position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetDirection(direction);
        }
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
