
using Photon.Pun;
using UnityEngine;

public class Movment : MonoBehaviourPun
{


    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Disable control for remote players
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<Movment>());
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        // Get movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }




}
