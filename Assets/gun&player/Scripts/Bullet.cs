using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPunCallbacks
{

    [SerializeField] private float speed = 25f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private int damage = 10;
    [SerializeField] private GameObject impactEffect;

    private Vector3 direction;
    private bool isConnected = false;

    private void Start()
    {
        if (direction == Vector3.zero) // Ensure a default direction
        {
            direction = transform.right;
        }

        if (photonView.IsMine)
        {
            Destroy(gameObject, lifeTime);
        }
    }

    private void Update()
    {
        transform.position += direction * Time.deltaTime * speed;
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        transform.right = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") )
        {

            PhotonView pv = collision.GetComponent<PhotonView>();

            if (pv != null) 
            {

                collision.GetComponent<player_hp_system>().TakeDamage(damage);
                PhotonNetwork.Instantiate(impactEffect.name, transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);

            }
            /*
            collision.GetComponent<player_hp_system>()?.TakeDamage(damage);
            PhotonNetwork.Instantiate(impactEffect.name, transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);
            */
        }
        else if (collision.CompareTag("Bullet") && photonView.IsMine)
        {
            PhotonView otherPhotonView = collision.GetComponent<PhotonView>();
            if (otherPhotonView != null && otherPhotonView.IsMine && !isConnected)
            {
                photonView.RPC("ConnectBullets", RpcTarget.All, otherPhotonView.ViewID);
            }
        }
    }
    [PunRPC]
    private void ConnectBullets(int otherBulletID)
    {
        GameObject otherBullet = PhotonView.Find(otherBulletID)?.gameObject;
        if (otherBullet == null) return;

        isConnected = true;

        GetComponent<SpriteRenderer>().color = Color.red;
        otherBullet.GetComponent<SpriteRenderer>().color = Color.red;

        speed = 0;
        otherBullet.GetComponent<Bullet>().speed = 0;

        PhotonNetwork.Instantiate(impactEffect.name, transform.position, Quaternion.identity);
    }
    /*

    [SerializeField] private float speed = 25f; 
    [SerializeField] private float lifeTime = 2f; 
    [SerializeField] private int damage = 10; 
    [SerializeField] private GameObject impactEffect; 

    private Vector3 direction;

    private void Start()
    {
        Destroy(gameObject, lifeTime); 
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
       // transform.position += direction * speed * Time.deltaTime; 
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        transform.right = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            collision.GetComponent<player_hp_system>().TakeDamage(damage);
            Instantiate(impactEffect, transform.position, Quaternion.identity); 
            Destroy(gameObject);
        }
    }
    */
}
