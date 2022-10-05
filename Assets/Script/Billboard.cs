using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


public class Billboard : MonoBehaviourPun
{
    [SerializeField] Text nickName;
    //[SerializeField] PhotonControl control; �����ֱ� ����

    
    void Start()
    {   
        // �ڽ��� �г������� �����ϴ� �����Դϴ�.
        nickName.text = photonView.Owner.NickName;
    }

    void Update()
    {
        // �ڱ� ���� ���⿡ ī�޶��� �� ������ �����Ͽ� �ٶ� �� �յ��� �����մϴ�.
        transform.forward = Camera.main.transform.forward;

        // ���ھ� ǥ�� //10-5
        // �ڽ��� �г������� �����ϴ� �����Դϴ�. ������ �Ծ����� 1�� ����
        //nickName.text = photonView.Owner.NickName + "( " + control.score + " ) "; �����ֱ� ����
    }
}
