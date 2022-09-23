using UnityEngine;
using UnityEngine.UI;
using TMPro; // textMeshPro 접근방식

public class PopUp : MonoBehaviour
{
    // text 없어질수 있음
    // textMeshPro 더 좋음
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;

    private static GameObject gamePanel; // 게임 패널 생성

    // PopUp 스트립트로 전역에서 접근할 수 있는 함수
    public static PopUp Show(string title, string message)
    {
        // 게임 패털이 아직 생성이 되지 않았다면
        if(gamePanel == null)
        {
            // 리소지스 폴더에 있는 게임 페널을 생성합니다.
            gamePanel = Instantiate(Resources.Load<GameObject>("Game Panel"));
            // 게임 페널에 생성한거 넣어주기
        }

        // 임시 오브젝트
        GameObject obj = Instantiate(gamePanel);

        // 팝업
        PopUp window = obj.GetComponent<PopUp>();
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
