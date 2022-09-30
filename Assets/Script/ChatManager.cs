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
            // �ƹ��͵� ä�� ���ϸ� �������Ͷ�
            if (input.text.Length == 0) return;

            // InputField �� �ִ� �ؽ�Ʈ�� �����ɴϴ�
            string chat = PhotonNetwork.NickName + " : " + input.text;

            // RpcTarget.All : ���� �뿡 �ִ� ��� Ŭ���̾�Ʈ���� RpcAddChat()�Լ��� �����϶�� ����Դϴ�
            photonView.RPC("RpcAddChat", RpcTarget.All, chat);
            //RpcTarget.Others�ٸ��������, master�ڱ����¸�
        }
    }

    [PunRPC] // �̰͸� ������ RPC �Լ��� ��
    void RpcAddChat(string message)
    {
        // ChatPrefab�� �ϳ� ���� text�� ���� �����մϴ�.
        GameObject chat = Instantiate(chatPrefab);
        chat.GetComponent<Text>().text = message;

        // ��ũ�� �� - content �ڽ����� ����մϴ�.
        chat.transform.SetParent(chatContent);

        // ä���� �Է��� �Ŀ��� �̾ �Է��Ҽ� �� ���� �����մϴ�.
        input.ActivateInputField();

        // input �ؽ�Ʈ �ʱ�ȭ�մϴ�.
        input.text = "";


    }

    
}
