using UnityEngine;
using UnityEngine.UI;
using TMPro; // textMeshPro ���ٹ��

public class PopUp : MonoBehaviour
{
    // text �������� ����
    // textMeshPro �� ����
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;

    private static GameObject gamePanel; // ���� �г� ����

    // PopUp ��Ʈ��Ʈ�� �������� ������ �� �ִ� �Լ�
    public static PopUp Show(string title, string message)
    {
        // ���� ������ ���� ������ ���� �ʾҴٸ�
        if(gamePanel == null)
        {
            // �������� ������ �ִ� ���� ����� �����մϴ�.
            gamePanel = Instantiate(Resources.Load<GameObject>("Game Panel"));
            // ���� ��ο� �����Ѱ� �־��ֱ�
        }

        // �ӽ� ������Ʈ
        GameObject obj = Instantiate(gamePanel);

        // �˾�
        PopUp window = obj.GetComponent<PopUp>();
        window.UpdateContent(title, message);

        return window;// �ڱ� �ڽ� ��ȯ
    }

    // �˾��� ������ ������Ʈ �ϴ� �Լ�
    public void UpdateContent(string titleMese, string contentMessage)
    {
        title.text = titleMese;
        content.text = contentMessage;
    }

    // �˾��� �ݴ� �Լ�
    public void Cancle()
    {
        Destroy(gameObject);
    }    

}
