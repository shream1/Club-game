using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public TMP_InputField  UserName;
    public TMP_Text Button;

    public void OnClickConnect() 
    {
    
        if(UserName.text.Length >1) 
        {
            
            PhotonNetwork.NickName = UserName.text;
            Button.text = "Joining...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        
        }
    
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }

}
