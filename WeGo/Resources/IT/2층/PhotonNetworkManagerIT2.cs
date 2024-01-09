using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonNetworkManagerIT2 : MonoBehaviourPunCallbacks
{
 
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room4", new RoomOptions { MaxPlayers = 6 }, null);

    public override void OnJoinedRoom()
        {
        PhotonNetwork.Instantiate("player", new Vector3(2.45f, -0.5f), Quaternion.identity);
    }
}