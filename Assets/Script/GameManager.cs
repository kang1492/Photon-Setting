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
        // 포톤 서버에서 오브젝트를 성생하는 방법
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

    public void CreateCharacter(string name) // 중복되는거 줄이는 함수.
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
        //PhotonNetwork.LeaveRoom(); 현재 룸에서 나가는 함수입니다.
        PhotonNetwork.LeaveRoom();
    }

    // 현재 플레이어가  룸에서 나갔다면 호출되는 함수
    public override void OnLeftRoom() //10-4
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }

}
