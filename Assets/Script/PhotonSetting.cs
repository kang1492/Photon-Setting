using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
//using TMPro;

public class PhotonSetting : MonoBehaviour
{
    [SerializeField] InputField email;
    [SerializeField] InputField password;
    [SerializeField] InputField username;
    [SerializeField] Dropdown region;


    private void Awake()
    {
        PlayFabSettings.TitleId = "664BF";
    }
    // 매개변수 LoginResult <- 로그인 성공 여부 반환합니다.

    public void LoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
        // AutomaticallySyncScene 마스터 클라이언트를 기준으로 씬을 동기화 할지 안할지 결정하는 기능
        // false = 동기화를 하지 않겟다
        // true = 마스터 클라이언트를 기준으로 동기화를 하겟다.
        PhotonNetwork.AutomaticallySyncScene = false;

        // 같은 버전의 유저끼리 접속을 허용합니다.
        // 같은 버전만 접속할 수 있도록 문자열 상수를 설정합니다.
        PhotonNetwork.GameVersion = "1.0f";

        // 유저 아이디 설정
        PhotonNetwork.NickName = username.text;

        // 입력한 지역을 설정합니다.                                                   // 0번째 지역
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.options[region.value].text;

        // 서버 접속
        PhotonNetwork.LoadLevel("Photon Lobby");

    }

    public void LoginFailure(PlayFabError error)
    {
        // 매개변수 2개
        PopUp.Show("LOGIN FAILURE", "Login Failed.\n Please login again");// 9-22

        //Debug.Log("로그인 실패");
    }

    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원 가입 성공");
    }

    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("회원 가입 실패");
    }

    public void SignUp()
    {
        // RegisterPlayFabUserRequest : 서버에 유저를 등록하기 위한 클래스
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,        // 입력한 Email
            Password = password.text,  // 입력한 비밀번호
            Username = username.text,  // 입력한 유저 이름
        };

        PlayFabClientAPI.RegisterPlayFabUser
            (
                request,        // 회원 가입에 대한 유저 정보
                SignUpSuccess,  // 회원 가입이 성공했을 때 회원 가입 성공 함수 호출
                SignUpFailure   // 회원 가입이 실패햇을 때 회원 가입 실패 함수 호출

            );

    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress
            (
            request,
            LoginSuccess,
            LoginFailure
            );
    }


}
