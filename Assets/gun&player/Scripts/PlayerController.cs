using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;

    SpriteRenderer sript;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<PlayerController>());
        }
        else
        {
            rb = GetComponent<Rigidbody2D>();
            sript = GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        Move();
        flip();
    }

    private void Move()
    {
        if (!photonView.IsMine) return;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveX, moveY).normalized * speed;
        rb.linearVelocity = movement;
    }


    private void flip() 
    {

        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.D)) 
            {
                
                sript.flipX = true;
            
            }
            else if (Input.GetKeyDown(KeyCode.A)) 
            {
                
                sript.flipX = false;
            
            }
        }

    
    }
}
