using UnityEngine;

public class bulletTrailScr : MonoBehaviour
{
    private Vector3 startPosision;
    private Vector3 targetPosision;
    private float progress;

    [SerializeField] private float speed = 40f;
    [SerializeField] private float damage = 10f; 
   
    void Start()
    {
       
       

        startPosision = transform.position;
        startPosision.z = -1; 
    }

    void Update()
    {
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPosision, targetPosision, progress);

      
        if (progress >= 1)
        {
            Destroy(gameObject); 
        }
    }

    public void SetTargetPosison(Vector3 targetPosision1)
    {
        targetPosision = targetPosision1;
        targetPosision.z = -1; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        IHittable hittable = collision.GetComponent<IHittable>();
        if (hittable != null)
        {
       
            hittable.Hit(damage);
            Destroy(gameObject); 
        }
    }
}
