using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; 
    [SerializeField] private float smoothSpeed = 5f; 
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f); 

    private void LateUpdate()
    {
        if (player != null)
        {
           
            Vector3 targetPosition = player.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
