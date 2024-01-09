using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonNetworkManagerIT21 : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room22", new RoomOptions { MaxPlayers = 6 }, null);

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("player", new Vector3(-2.09f, -2.07f), Quaternion.identity);
    }

}