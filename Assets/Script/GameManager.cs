using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    int count;

    void Start()
    {
        switch (DataManager.characterCount)
        {
            case 0: CreateCharacter("Character");
        // ���� �������� ������Ʈ�� �����ϴ� ���
        //PhotonNetwork.Instantiate
        //    (
        //    "Character",
        //    new Vector3
        //        (
        //        Random.Range(0, 5),
        //        1,
        //        Random.Range(0, 5)
        //        ),
        //    Quaternion.identity
        //    );
                break;
            case 1: CreateCharacter("Children");
                break;
            case 2: CreateCharacter("Researcher");
                break;
        }
    }

    public void CreateCharacter(string name) // �ߺ��Ǵ°� ���̴� �Լ�.
    {
        PhotonNetwork.Instantiate
                   (
                   name,
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
