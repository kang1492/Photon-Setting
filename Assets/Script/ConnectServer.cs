using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectServer : MonoBehaviourPunCallbacks
{
    private string serverName;//10-7
    [SerializeField] GameObject[] character;//10-11

    private void Start() //10-11
    {
        // 캐릭터1, 캐릭터2, 캐릭터3
        character[DataManager.characterCount].SetActive(true);
    }

    public void RightCharacterSelect()
    {
        DataManager.characterCount++; //0 이였다가 버턴클릭하느 순간 1

        // for 문을 돌면서 게임 오브젝트(캐릭터) 전체를 비활성화 합니다.
        for(int i = 0; i < character.Length; i++)
        {
            // 0 : Exo Gray
            // 1 : Aj
            // 2 : ch11_non
            character[i].SetActive(false);// 처음에는 비활성화 해주기
        }
        
        // character[1] 만 활성화 상태 -> 화면에 보임
        character[DataManager.characterCount].SetActive(true);

        if (DataManager.characterCount >= 2 )
        {
            DataManager.characterCount = -1;
        }
    }

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
