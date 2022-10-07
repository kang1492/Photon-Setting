using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectServer : MonoBehaviourPunCallbacks
{
    private string serverName;//10-7
    public void SelectLobby(string text) //10-7
    {
        // Challeger Server
        serverName = text; //10-7

        // 서버접속
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }

    public override void OnConnectedToMaster()
    {

        PhotonNetwork.JoinLobby(new TypedLobby(serverName, LobbyType.Default));
    }

}
