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
}
