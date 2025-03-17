using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.UI;

public class LobbyManger : MonoBehaviourPunCallbacks
{
   public InputField RoomInputeField;
    public GameObject LobbyPan;
    public GameObject RoomPan;

    public Text RoomName;


    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdate = 1.5f;
    public float nextUpdatTime;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem PlayerItemPerPrefab;
    public Transform PlayerItemParent;


    public GameObject playButton;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }



  public  void OnClickCreate() 
    {

        if (RoomInputeField.name.Length > 1) 
        {

            PhotonNetwork.CreateRoom(RoomInputeField.name , new RoomOptions() { MaxPlayers = 30 , BroadcastPropsChangeToAll = true} );
        
        }
    
    }

    public override void OnJoinedRoom()
    {
        LobbyPan.SetActive(false);
        RoomPan.SetActive(true);
        RoomName.name = "Room Name" + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }



    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdatTime)
        {
            updateRoomList(roomList);
            nextUpdatTime = Time.time + timeBetweenUpdate;
        }
    }


    void updateRoomList(List<RoomInfo> List) 
    {

        foreach(RoomItem item in roomItemsList) 
        {
            Destroy(item.gameObject);
        
        }
        roomItemsList.Clear();

        foreach(RoomInfo roomInfo in List) 
        {
          RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(roomInfo.Name);
            roomItemsList.Add(newRoom);
        }
    
    }


    public void JoinRoom(string roomName) 
    {
        
        PhotonNetwork.JoinRoom(roomName);
    
    }


    public void onClickLeaveRoom() 
    {

        PhotonNetwork.LeaveRoom();
    
    
    }

    public override void OnLeftRoom()
    {
        RoomPan.SetActive(false) ;
        LobbyPan.SetActive(true) ;
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }


    void UpdatePlayerList() 
    {
        
        foreach(PlayerItem item in playerItemsList) 
        {
            Destroy(item.gameObject);
        
        }
        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null) 
        {

            return;
        
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players) 
        {

            PlayerItem newPlayerItem = Instantiate(PlayerItemPerPrefab, PlayerItemParent);
            newPlayerItem.setPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer) 
            {
                
                newPlayerItem.ApplyLocalChange();
            
            }

            playerItemsList.Add(newPlayerItem);
        
        }


    
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient) 
        {
            
            playButton.SetActive(true);
        
        }
        else 
        {
        
            playButton.SetActive(false);

        }
    }

    public void OnClickPlayButton() 
    {

        PhotonNetwork.LoadLevel("Game");
    
    }



}
