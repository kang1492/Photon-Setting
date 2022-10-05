using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


public class Billboard : MonoBehaviourPun
{
    [SerializeField] Text nickName;
    //[SerializeField] PhotonControl control; 보여주기 시험

    
    void Start()
    {   
        // 자신의 닉네임으로 설정하는 변수입니다.
        nickName.text = photonView.Owner.NickName;
    }

    void Update()
    {
        // 자기 앞의 방향에 카메라의 앞 방향을 지정하여 바라볼 수 잇도록 설정합니다.
        transform.forward = Camera.main.transform.forward;

        // 스코어 표시 //10-5
        // 자신의 닉네임으로 설정하는 변수입니다. 아이템 먹었을때 1이 들어간다
        //nickName.text = photonView.Owner.NickName + "( " + control.score + " ) "; 보여주기 시험
    }
}
