using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public Button connect;
    public Text currentRegion;
    public Text currentLobby;

    public void Connect() // �����ϴٴ� �ǹ�
    {
        // ���� ����
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Update()
    {
        // ���� ���� ������ ��ϵ˴ϴ�. �տ��� ������ ���ұ� ������ �ٲܼ� ����.
        currentRegion.text = PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion;

        switch (Data.count)
        {
            // �κ� ����
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
    // ��� ������ �θ� ����ϱ�
    // ���� ������ ���� �� ȣ��Ǵ� �ݹ� �Լ�
    // �κ� �����ߴ��� Ȯ���� �� �ִ� �Լ��Դϴ�.
    public override void OnConnectedToMaster() // 9-23
    {
        // Ư�� �κ� �����Ͽ� �����ϴ� ���
        switch(Data.count)
        {                                               // �κ� �̸� , ���ȣ
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

    // �κ� ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedLobby()
    {
        // PhotonNetwork.LoadLevel�� ����ϴ� ������ ��ư ��Ʈ��ũ���� ���� ����ȭ�� ���߱� ���� ����մϴ�.
        PhotonNetwork.LoadLevel("Photon Room");
    }
}
