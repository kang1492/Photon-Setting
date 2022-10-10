using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text scoreText;
    public Text leaderBorderText;
    [SerializeField] GameObject leaderBorder; //10-6

    private void Awake() // ��ŸƮ �Լ��� ����Ǳ� ���� �����ũ�� �� �޾Ƴ���
    {
        if(instance == null)
        {
            instance = this;
        }

        // L value �� R value
        // L value : ǥ���� ���Ŀ��� ������� ���� �� ( �޸� ������ ������ �ִ� ����)
        // R value : ǥ���� ���Ŀ��� ������� �� ( �ӽ� ���� )
        int box = 10;
    }

    public void GetLeaderBorder()
    {
        leaderBorder.SetActive(true); //10-6

        // var : �ڵ����� �ڷ����� �ڷ����� �����ݴϴ�
        var request = new GetLeaderboardRequest
        {
            StartPosition = 0, // �⺻ ��ġ ��
            StatisticName = "Score", // Playfab���� �ҷ��� ����ǥ �̸�
            MaxResultsCount = 20, // ����ǥ�� �ִ�� ��Ÿ���� ��
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                // ��ġ
                ShowLocations = true,
                ShowDisplayName = true
                // �÷��̾� ID Ÿ��Ʋ
            }
        };

        PlayFabClientAPI.GetLeaderboard(request, (result) =>
        {
            // �� �ҷ�����
            for (int i = 0; i < result.Leaderboard.Count; i++)
            {
                var currentBoader = result.Leaderboard[i];
                leaderBorderText.text += currentBoader.Profile.Locations[0].CountryCode.Value +
                " - " + currentBoader.DisplayName + " - " + currentBoader.StatValue + "\n";
            }
        },
        (error) => Debug.Log("�������带 �ҷ����� ���߽��ϴ�."));
    }

    public void GetVirtualCurrency()
    {
        var request = new AddUserVirtualCurrencyRequest()
        {
            VirtualCurrency = "RP", // Playfab �� ������ ��ȭ �ڵ�
            Amount = 100 // ����ȭ�п� �߰��� ��
        };

        PlayFabClientAPI.AddUserVirtualCurrency
        (                                                       // ���� ��
            request, (result) => Debug.Log("���� �߰��Ǿ����ϴ�." + result.Balance),
            (error) => Debug.Log("���� ȭ�� ȹ�椿�� ���߽��ϴ�.")
        );
    }
}
