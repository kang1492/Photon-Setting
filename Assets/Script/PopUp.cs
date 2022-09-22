using UnityEngine;
using UnityEngine.UI;
using TMPro; // textMeshPro ���ٹ��

public class PopUp : MonoBehaviour
{
    // text �������� ����
    // textMeshPro �� ����
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;

    private static GameObject gamePanel;

    // PopUp ��Ʈ��Ʈ�� �������� ������ �� �ִ� �Լ�
    public static PopUp Show(string title, string message)
    {
        if(gamePanel == null)
        {
            Instantiate(Resources.Load<GameObject>("Game Panel"));
        }  

        // �˾�
        PopUp window = gamePanel.GetComponent<PopUp>();
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
