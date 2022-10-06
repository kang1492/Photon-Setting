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

    private void Awake() // 스타트 함수가 실행되기 전에 어웨이크로 다 받아놓기
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void GetLeaderBorder()
    {
        leaderBorder.SetActive(true); //10-6

        // var : 자동으로 자료형을 자료형을 정해줍니다
        var request = new GetLeaderboardRequest
        {
            StartPosition = 0, // 기본 위치 값
            StatisticName = "Score", // Playfab에서 불러올 순위표 이름
            MaxResultsCount = 20, // 순위표에 최대로 나타나는 수
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                // 위치
                ShowLocations = true,
                ShowDisplayName = true
                // 플레이어 ID 타이틀
            }
        };

        PlayFabClientAPI.GetLeaderboard(request, (result) =>
        {
            // 다 불러오기
            for (int i = 0; i < result.Leaderboard.Count; i++)
            {
                var currentBoader = result.Leaderboard[i];
                leaderBorderText.text += currentBoader.Profile.Locations[0].CountryCode.Value +
                " - " + currentBoader.DisplayName + " - " + currentBoader.StatValue + "\n";
            }
        },
        (error) => Debug.Log("리더보드를 불러오지 못했습니다."));
    }
}
