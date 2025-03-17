using UnityEngine;

using UnityEngine.UI;


public class RoomItem : MonoBehaviour
{
    public Text roomName;

    public LobbyManger manger;

    private void Start()
    {
        manger = FindAnyObjectByType<LobbyManger>();
    }

    public void SetRoomName(string _roomName) 
    {
        
        roomName.text = _roomName;
    
    }

    public void onClickItem() 
    {

        manger.JoinRoom(roomName.text);
    
    }


}
