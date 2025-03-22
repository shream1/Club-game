using Photon.Realtime;
using UnityEngine;
using Photon.Pun;

public class GunAim : MonoBehaviourPun
{
    [SerializeField] private Transform player;
    [SerializeField] private float orbitRadius = 4.4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<GunAim>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            RotateWeaponWithMouse();
        }
    }
    Vector3 direction;
    public float distanceFromPlayer;


    private void RotateWeaponWithMouse()
    {
        if (photonView.IsMine)
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
    }
}
