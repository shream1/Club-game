using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject[] playerPrefabs;
    public Transform[] spawnNodes;


    public void Start()
    {
        int randomNumber = Random.Range(0, spawnNodes.Length);
        Transform spawnNode = spawnNodes[randomNumber];
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnNode.position,Quaternion.identity);
    }

   public void spawn() 
    {
        int randomNumber = Random.Range(0, spawnNodes.Length);
        Transform spawnNode = spawnNodes[randomNumber];
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnNode.position, Quaternion.identity);


    }

}
