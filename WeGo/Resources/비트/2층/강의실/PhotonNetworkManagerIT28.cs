using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonNetworkManagerIT28 : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room29", new RoomOptions { MaxPlayers = 6 }, null);

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("player", new Vector3(-0.86f, 1.49f), Quaternion.identity);
    }

}