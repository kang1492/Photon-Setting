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

    private void Awake() // ��ŸƮ �Լ��� ����Ǳ� ���� �����ũ�� �� �޾Ƴ���
    {
        if(instance == null)
        {
            instance = this;
        }
    }

}
