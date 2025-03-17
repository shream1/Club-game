using UnityEngine;

public class SimpleMoveForTest : MonoBehaviour
{
    //Static Stuff
    public float speed;
    public Rigidbody2D rb;
    float x, y;
    Vector2 directions;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        TakeInputeSystem();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void TakeInputeSystem()
    {
        x = Input.GetAxisRaw("Horizontal") * speed;
        y = Input.GetAxisRaw("Vertical") * speed;

    }

    void Move()
    {

        rb.linearVelocity = new Vector2(x, y);


    }
}
