using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime; // ��� ������ �������� �� �̺�Ʈ�� ȣ���ϴ� ���̺귯��

public class RoomManager : MonoBehaviourPunCallbacks // �̰��ؾ� ��Ī ����Ҽ� ����
{
    [SerializeField] InputField roomName, roomPerson;
    [SerializeField] Button roomCreate, roomJoin;

    [SerializeField] GameObject roomPrefab;
    [SerializeField] Transform roomContent;


    // �� ���� 
    // �ð� ���⵵�� 0(1)�� �ð� ���⵵�� �����ϴ�
    // Dictionary : key-value ������ ���� ������ �� �ִ� �ڷᱸ���Դϴ�.
    Dictionary<string, RoomInfo> roomCatalog = new Dictionary<string, RoomInfo>();


    // Update is called once per frame
    void Update()
    {
        // �� �̸��� �ϳ��� �Է��޴ٸ�
        if(roomName.text.Length > 0)
        {
            // �� ���� ��ư�� Ȱ��ȭ�մϴ�.
            roomJoin.interactable = true;
        }
        else // �� �̸��� �ϳ��� �Է����� �ʾҴٸ�
        {
            roomJoin.interactable = false;
        }

        // OnJoinRoomFailed : �� ���ӿ� �������� �� ȣ��Ǵ� �Լ�
        // �� ���ӿ� �����ϰ� �Ǹ� �ڵ����� �ϳ� �����ؼ� ���� ����� �ֽ��ϴ�.
        
        // �� �̸��� �� �ο��� �Է����� ������
        if(roomName.text.Length > 0 && roomPerson.text.Length > 0)
        {
            // �� ���� ��ư Ȱ��ȭ
            roomCreate.interactable = true;
        }
        else
        {
            // �� ���� ��ư ��Ȱ��ȭ
            roomCreate.interactable = false;
        }
    }

    // ���� ���� �ִ� 10������� ������ �� �ֵ��� ������ �� �ֽ��ϴ�.
    public void OnlickCreatRoom()
    {
        // �� �ɼ��� �����մϴ�.
        RoomOptions Room = new RoomOptions();

        // �ִ� �������� ���� �����մϴ�.
        Room.MaxPlayers = byte.Parse(roomPerson.text);

        // ���� ���� ���θ� �����մϴ�.
        Room.IsOpen = true;

        // �κ񿡼� �� ����� ���� ��ų�� �����մϴ�.
        Room.IsVisible = true;

        // ���� �����ϴ� �Լ�
        PhotonNetwork.CreateRoom(roomName.text, Room);
    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    // ���� �����Ǿ��� �� ȣ��Ǵ� �ݹ��Լ�
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
    }

    // �� ���� �Լ�
    public void AllDeleteRoom()
    {
        // Transform ������Ʈ(Content)�� �մ� ���� ������Ʈ�� �����Ͽ� ��ü ���� �����մϴ�.
        //                elemnt <- [room] �־��ְ� ������ ����
        foreach(Transform element in roomContent)
        {
            // Transform�� ������ �ִ� ���� ������Ʈ�� �����մϴ٤�
            Destroy(transform.gameObject);
        }
    }

    // �뿡 ������ �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void CreateRoomObject()
    {
        // RoomCatalog �� ���� ���� Value���� �� �ִٸ� Roominfo�� �־��ݴϴ�.
        foreach(RoomInfo info in roomCatalog.Values)
        {
            // ���� �����մϴ�.
            GameObject room = Instantiate(roomPrefab);

            // RoomContect�� ���� ������Ʈ�� �����մϴ�.
            room.transform.SetParent(roomContent);

            // �� ������ �Է��մϴ�.
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    // �� ������ �ı� , �� ���� ����, �� ����� ������Ʈ �ϴ� �Լ�
    public void UpdateRoom(List<RoomInfo> roomList)
    {
        // [0] <- ó�� �����
        // [1] <- �ι�° �����
        for(int i = 0; i < roomList.Count; i++)
        {
            // �ش� �̸��� RoomCatalog�� key ������ �����Ǿ� �ִٸ�
            if(roomCatalog.ContainsKey(roomList[i].Name))
            {
                // RemovedFromList : (true) �뿡�� ������ �Ǿ�����
                if (roomList[i].RemovedFromList) // �濡�� ��� ��������
                {
                    // ��ųʸ��� �մ� �����͸� �����մϴ�.
                    roomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }
            // ���� ���ٸ� roominfo�� RoomCatalog�� �߰��մϴ�.
            roomCatalog[roomList[i].Name] = roomList[i];
        }
    }

    // �ش� �κ� �� ����� ���� ������ ������ ȣ��(�߰�, ����, ����)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }
}
