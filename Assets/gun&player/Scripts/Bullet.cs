using UnityEngine;

public class Bullet : MonoBehaviour
{
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
}
