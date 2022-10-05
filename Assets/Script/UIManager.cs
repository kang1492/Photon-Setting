using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //public Text scoreText;
    //public Text leaderBorderText;

    private void Awake() // 스타트 함수가 실행되기 전에 어웨이크로 다 받아놓기
    {
        if(instance == null)
        {
            instance = this;
        }
    }

}
