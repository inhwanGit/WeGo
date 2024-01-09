using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonNetworkManagerWoosongTower: MonoBehaviourPunCallbacks
{
 
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room1", new RoomOptions { MaxPlayers = 6 }, null);

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("player(campus)", new Vector3(-15.1f, -0.78f), Quaternion.identity);
    }
}