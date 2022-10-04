using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    
    void Start()
    {
        PhotonNetwork.Instantiate
            (
            "Character",
            new Vector3
                (
                Random.Range(0, 5),
                1,
                Random.Range(0, 5)
                ),
            Quaternion.identity
            );
    }

    public void ExitRoom() //10-4
    {   
        //PhotonNetwork.LeaveRoom(); ���� �뿡�� ������ �Լ��Դϴ�.
        PhotonNetwork.LeaveRoom();
    }

    // ���� �÷��̾  �뿡�� �����ٸ� ȣ��Ǵ� �Լ�
    public override void OnLeftRoom() //10-4
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }

}
