using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime; // 어느 서버에 접속했을 때 이벤트를 호출하는 라이브러리

public class RoomManager : MonoBehaviourPunCallbacks // 이걸해양 매칭 사용할수 잇음
{
    [SerializeField] InputField roomName, roomPerson;
    [SerializeField] Button roomCreate, roomJoin;

    [SerializeField] GameObject roomPrefab;
    [SerializeField] Transform roomContent;


    // 맵 구조 
    // 시간 복잡도가 0(1)의 시간 복잡도를 가집니다
    // Dictionary : key-value 형태의 값을 저장할 수 있는 자료구조입니다.
    Dictionary<string, RoomInfo> roomCatalog = new Dictionary<string, RoomInfo>();


    // Update is called once per frame
    void Update()
    {
        // 방 이름을 하나라도 입력햇다면
        if(roomName.text.Length > 0)
        {
            // 방 접속 버튼을 활성화합니다.
            roomJoin.interactable = true;
        }
        else // 방 이름을 하나라도 입력하지 않았다면
        {
            roomJoin.interactable = false;
        }

        // OnJoinRoomFailed : 방 접속에 실패했을 때 호출되는 함수
        // 방 접속에 실패하게 되면 자동으로 하나 생성해서 들어가는 방법이 있습니다.
        
        // 방 이름과 방 인원을 입력하지 않으면
        if(roomName.text.Length > 0 && roomPerson.text.Length > 0)
        {
            // 방 생성 버튼 활성화
            roomCreate.interactable = true;
        }
        else
        {
            // 방 생성 버튼 비활성화
            roomCreate.interactable = false;
        }
    }

    // 포톤 룸은 최대 10명까지만 접속할 수 있도록 설정할 수 있습니다.
    public void OnlickCreatRoom()
    {
        // 룸 옵션을 설정합니다.
        RoomOptions Room = new RoomOptions();

        // 최대 접속자의 수를 설정합니다.
        Room.MaxPlayers = byte.Parse(roomPerson.text);

        // 룸의 접속 여부를 설정합니다.
        Room.IsOpen = true;

        // 로비에서 룸 목록을 노출 시킬지 설정합니다.
        Room.IsVisible = true;

        // 룸을 생성하는 함수
        PhotonNetwork.CreateRoom(roomName.text, Room);
    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    // 룸이 생성되었을 때 호출되는 콜백함수
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
    }

    // 룸 삭제 함수
    public void AllDeleteRoom()
    {
        // Transform 오브젝트(Content)에 잇는 하위 오브젝트에 접근하여 전체 룸을 삭제합니다.
        //                elemnt <- [room] 넣어주고 나머지 삭제
        foreach(Transform element in roomContent)
        {
            // Transform이 가지고 있는 게임 오브젝트를 삭제합니다ㅣ
            Destroy(transform.gameObject);
        }
    }

    // 룸에 입작한 후 호출되는 콜백 함수
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void CreateRoomObject()
    {
        // RoomCatalog 에 여러 개의 Value값이 들어가 있다면 Roominfo에 넣어줍니다.
        foreach(RoomInfo info in roomCatalog.Values)
        {
            // 룸을 생성합니다.
            GameObject room = Instantiate(roomPrefab);

            // RoomContect의 하위 오브젝트롤 설정합니다.
            room.transform.SetParent(roomContent);

            // 룸 정보를 입력합니다.
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    // 방 나가면 파괴 , 방 생성 관리, 룸 목록을 업데이트 하는 함수
    public void UpdateRoom(List<RoomInfo> roomList)
    {
        // [0] <- 처음 만든방
        // [1] <- 두번째 만든방
        for(int i = 0; i < roomList.Count; i++)
        {
            // 해당 이름이 RoomCatalog의 key 값으로 설정되어 있다면
            if(roomCatalog.ContainsKey(roomList[i].Name))
            {
                // RemovedFromList : (true) 룸에서 삭제가 되었을때
                if (roomList[i].RemovedFromList) // 방에서 모두 나갔을때
                {
                    // 딕셔너리에 잇는 데이터를 삭제합니다.
                    roomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }
            // 룸이 없다면 roominfo를 RoomCatalog에 추가합니다.
            roomCatalog[roomList[i].Name] = roomList[i];
        }
    }

    // 해당 로비에 방 목록의 변경 사항이 있으면 호출(추가, 삭제, 참가)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }
}
