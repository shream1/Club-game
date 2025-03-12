using UnityEngine;

public class Movment : MonoBehaviour
{

     public  float speed;
     float x;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        takeInpute();
    }
    private void FixedUpdate()
    {
        move();
    }

    void takeInpute() 
    {

        x = Input.GetAxisRaw("Horizontal");
    
    }

    void move() 
    {

        rb.linearVelocity = new Vector2(x * speed * Time.deltaTime,0);
    
    }
}
