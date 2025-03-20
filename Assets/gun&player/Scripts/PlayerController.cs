using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<PlayerController>());
        }
        else
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        Move();
    }

    private void Move()
    {
        if (!photonView.IsMine) return;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveX, moveY).normalized * speed;
        rb.linearVelocity = movement;
    }
}
