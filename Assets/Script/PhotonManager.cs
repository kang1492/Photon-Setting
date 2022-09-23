using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public Button connect;
    public Text currentRegion;
    public Text currentLobby;

    public void Connect() // 연결하다는 의미
    {
        // 서버 접속
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Update()
    {
        // 현재 국가 정보가 등록됩니다. 앞에서 새팅해 놓았기 때문에 바꿀수 없다.
        currentRegion.text = PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion;

        switch (Data.count)
        {
            // 로비 선택
            case 0: 
                currentLobby.text = "First Lobby";
                break;
            case 1:
                currentLobby.text = "Second Lobby";
                break;
            case 2:
                currentLobby.text = "Third Lobby";
                break;

        }
    }
    // 상속 했을때 부모꺼 사용하기
    // 포톤 서버에 접속 후 호출되는 콜백 함수
    // 로비에 접속했는지 확인할 수 있는 함수입니다.
    public override void OnConnectedToMaster() // 9-23
    {
        // 특정 로비를 생성하여 진입하는 방법
        switch(Data.count)
        {                                               // 로비 이름 , 방번호
            case 0: 
                PhotonNetwork.JoinLobby(new TypedLobby("Lobby 1", LobbyType.Default));
                break;
            case 1:
                PhotonNetwork.JoinLobby(new TypedLobby("Lobby 2", LobbyType.Default));
                break;
            case 2:
                PhotonNetwork.JoinLobby(new TypedLobby("Lobby 3", LobbyType.Default));
                break;
        }
    }

    // 로비에 접속 후 호출되는 콜백 함수
    public override void OnJoinedLobby()
    {
        // PhotonNetwork.LoadLevel을 사용하는 이유는 포튼 네트워크에서 씬의 동기화를 맞추기 위해 사용합니다.
        PhotonNetwork.LoadLevel("Photon Room");
    }
}
