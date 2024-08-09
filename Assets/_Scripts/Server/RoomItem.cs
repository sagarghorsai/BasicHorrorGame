using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomItem : MonoBehaviour
{
    public TMP_Text roomName;
    LobbyManager manager;

    private void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomname(string _roomName)
    {
        roomName.text = _roomName;
    }
    public void OnClickItem()
    {
        manager.JoinRoom(roomName.text);
    }


}
