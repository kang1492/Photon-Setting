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


    void Start()
    {
        
    }
    // �Ű����� LoginResult <- �α��� ���� ���� ��ȯ�մϴ�.

    public void LoginSuccss(LoginResult result)
    {
        // AutomaticallySyncScene ������ Ŭ���̾�Ʈ�� �������� ���� ����ȭ ���� ������ �����ϴ� ���
        // false = ����ȭ�� ���� �ʰٴ�
        // true = ������ Ŭ���̾�Ʈ�� �������� ����ȭ�� �ϰٴ�.
        PhotonNetwork.AutomaticallySyncScene = false;

        // ���� ������ �������� ������ ����մϴ�.
        // ���� ������ ������ �� �ֵ��� ���ڿ� ����� �����մϴ�.
        PhotonNetwork.GameVersion = "1.0f";

        // ���� ���̵� ����
        PhotonNetwork.NickName = username.text;

        // �Է��� ������ �����մϴ�.                                                   // 0��° ����
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.options[region.value].text;

    }

    public void LoginFailure(PlayFabError error)
    {
        Debug.Log("�α��� ����");
    }

    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("ȸ�� ���� ����");
    }

    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("ȸ�� ���� ����");
    }

    public void SignUp()
    {
        // RegisterPlayFabUserRequest : ������ ������ ����ϱ� ���� Ŭ����
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,        // �Է��� Email
            Password = password.text,  // �Է��� ��й�ȣ
            Username = username.text,  // �Է��� ���� �̸�
        };

        PlayFabClientAPI.RegisterPlayFabUser
            (
                request,        // ȸ�� ���Կ� ���� ���� ����
                SignUpSuccess,  // ȸ�� ������ �������� �� ȸ�� ���� ���� �Լ� ȣ��
                SignUpFailure   // ȸ�� ������ �������� �� ȸ�� ���� ���� �Լ� ȣ��

            );
    }
}
