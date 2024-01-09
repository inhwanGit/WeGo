using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonNetworkManager1 : MonoBehaviourPunCallbacks
{
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();             
    }

    public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room1", new RoomOptions { MaxPlayers = 6 }, null);

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("player(campus)", new Vector3(0.26f, -8.55f), Quaternion.identity);
    }
}