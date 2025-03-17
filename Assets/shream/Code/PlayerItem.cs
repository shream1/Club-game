using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public Text PlayerName;

   public Image backGround;
    public Color hightLite;
    public GameObject leftArrowKey;
    public GameObject rightArrowKey;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerAvatar;
    public Sprite[] avatars;

    Player player;

    private void Start()
    {
        backGround = GetComponent<Image>();
        
    }
    public void setPlayerInfo(Player _player) 
    {
        
        PlayerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItems(player );
    
    }


    public void ApplyLocalChange() 
    {
        
        backGround.color = hightLite;
        leftArrowKey.SetActive(true);
       // rightArrowKey.SetActive(false);
    
    }

    public void OnClickLeftArrow() 
    {

        if ((int)playerProperties["playerAvatar"] == 0) 
        {

            playerProperties["playerAvatar"] = avatars.Length - 1;
            
        }
        else 
        {

            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;


        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow() 
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
        {

            playerProperties["playerAvatar"] = 0;

        }
        else
        {

            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;


        }

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {

        if (player == targetPlayer) 
        {

            UpdatePlayerItems(targetPlayer);


        }
       
    }

    void UpdatePlayerItems(Player player) 
    {

        if (player.CustomProperties.ContainsKey("playerAvatar")) 
        {

            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        
        }
        else 
        {
            playerProperties["playerAvatar"] = 0;
        
        }
    
    }


}
