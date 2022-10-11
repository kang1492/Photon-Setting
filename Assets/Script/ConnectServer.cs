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
        // ĳ����1, ĳ����2, ĳ����3
        character[DataManager.characterCount].SetActive(true);
    }

    public void RightCharacterSelect()
    {
        DataManager.characterCount++; //0 �̿��ٰ� ����Ŭ���ϴ� ���� 1

        // for ���� ���鼭 ���� ������Ʈ(ĳ����) ��ü�� ��Ȱ��ȭ �մϴ�.
        for(int i = 0; i < character.Length; i++)
        {
            // 0 : Exo Gray
            // 1 : Aj
            // 2 : ch11_non
            character[i].SetActive(false);// ó������ ��Ȱ��ȭ ���ֱ�
        }
        
        // character[1] �� Ȱ��ȭ ���� -> ȭ�鿡 ����
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

        // ��������
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
