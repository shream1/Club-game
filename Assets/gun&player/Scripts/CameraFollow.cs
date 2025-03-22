using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class CameraFollow : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform player; 
    [SerializeField] private float smoothSpeed = 5f; 
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);

    private void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<CameraFollow>());

        }
        if (photonView.IsMine) 
        {
            
            this.GetComponent<CameraFollow>().enabled = true;

        }
    }

    private void LateUpdate()
    {
        if (photonView.IsMine)
        {
            

                Vector3 targetPosition = player.position + offset;

                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            
        }
    }
}
