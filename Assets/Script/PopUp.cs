using UnityEngine;
using UnityEngine.UI;
using TMPro; // textMeshPro 접근방식

public class PopUp : MonoBehaviour
{
    // text 없어질수 있음
    // textMeshPro 더 좋음
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;

    private static GameObject gamePanel;

    // PopUp 스트립트로 전역에서 접근할 수 있는 함수
    public static PopUp Show(string title, string message)
    {
        if(gamePanel == null)
        {
            Instantiate(Resources.Load<GameObject>("Game Panel"));
        }  

        // 팝업
        PopUp window = gamePanel.GetComponent<PopUp>();
        window.UpdateContent(title, message);

        return window;// 자기 자신 반환
    }

    // 팝업의 내용을 업데이트 하는 함수
    public void UpdateContent(string titleMese, string contentMessage)
    {
        title.text = titleMese;
        content.text = contentMessage;
    }

    // 팝업을 닫는 함수
    public void Cancle()
    {
        Destroy(gameObject);
    }    

}
