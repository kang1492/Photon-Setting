using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.EventSystems; // ��

//using TMPro;


public class PhotonSetting : MonoBehaviour
{
    EventSystem eventSystem; // ��
    public Selectable firstInput;// ó���� ���� �Ǵ� Ŀ��

    [SerializeField] InputField email;
    [SerializeField] InputField password;
    [SerializeField] InputField username;
    [SerializeField] Dropdown region;


    private void Awake()
    {
        eventSystem = EventSystem.current;
        // ó���� Email Input Field�� �����ϵ��� �����մϴ�.
        firstInput.Select(); // ��

        PlayFabSettings.TitleId = "664BF";
    }
    // �Ű����� LoginResult <- �α��� ���� ���� ��ȯ�մϴ�.

    private void Update() // Ű �Է��� 
    {
        if(Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            // TAB + LeftShift�� ���� Selectable ��ü�� �����մϴ�.
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();

            if(next != null)
            {
                next.Select();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if(next != null)
            {
                next.Select();
            }
        }

    }

    public void LoginSuccess(LoginResult result)
    {
        Debug.Log("�α��� ����");
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

        // ���� ����
        PhotonNetwork.LoadLevel("Photon Lobby");

    }

    public void LoginFailure(PlayFabError error)
    {
        // �Ű����� 2��
        PopUp.Show
            (
            "MEMBERSHIP\nSUCCESSFUL", 
            "Congratulations on your\nSuccessful Membership Registration"
            );// 9-22

        //Debug.Log("�α��� ����");
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
            DisplayName = username.text
            // ���� ǥ�� ���̵� ���̰�
        };

        PlayFabClientAPI.RegisterPlayFabUser
            (
                request,        // ȸ�� ���Կ� ���� ���� ����
                SignUpSuccess,  // ȸ�� ������ �������� �� ȸ�� ���� ���� �Լ� ȣ��
                SignUpFailure   // ȸ�� ������ �������� �� ȸ�� ���� ���� �Լ� ȣ��

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
