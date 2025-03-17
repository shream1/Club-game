
using UnityEngine;

public class Movment : MonoBehaviour {


     public  float speed;
     float x;
     float y;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    
    void Update()
    {
        
        {
            takeInpute();
        }
    }
  
    private void FixedUpdate()
    {
        
        {
            move();
        }
       
    }
    
    void takeInpute() 
    {

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    
    }
    
    void move() 
    {

        RpcCommad();
    
    }

    
    void  RpcCommad() 
    {
        rb.linearVelocity = new Vector2(x * speed * 10 * Time.deltaTime, y * speed * 10 * Time.deltaTime);
    }



}
