using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField input;
    [SerializeField] GameObject chatPrefab;
    [SerializeField] Transform chatContent;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            // 아무것도 채팅 안하면 빠져나와라
            if (input.text.Length == 0) return;

            // InputField 에 있는 텍스트를 가져옵니다
            string chat = PhotonNetwork.NickName + " : " + input.text;

            // RpcTarget.All : 현재 룸에 있는 모든 클라이언트에게 RpcAddChat()함수를 실행하라는 명령입니다
            photonView.RPC("RpcAddChat", RpcTarget.All, chat);
            //RpcTarget.Others다른사람한태, master자기한태만
        }
    }

    [PunRPC] // 이것만 넣으면 RPC 함수가 됨
    void RpcAddChat(string message)
    {
        // ChatPrefab을 하나 만들어서 text에 값을 설정합니다.
        GameObject chat = Instantiate(chatPrefab);
        chat.GetComponent<Text>().text = message;

        // 스크롤 뷰 - content 자식으로 등록합니다.
        chat.transform.SetParent(chatContent);

        // 채팅을 입력한 후에도 이어서 입력할수 있 도록 설정합니다.
        input.ActivateInputField();

        // input 텍스트 초기화합니다.
        input.text = "";


    }

    
}
