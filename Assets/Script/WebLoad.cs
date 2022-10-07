using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;// �̹��� �ҷ�����


public class WebLoad : MonoBehaviour
{
    // ������ �̹����� �ҷ��� ���� Raw Image�� ����ؾ� �մϴ�.
    [SerializeField] RawImage [] webImage; // �̹��� ������ �ޱ�
    //[SerializeField] RawImage webImage; �̹��� 1�� �ޱ�

    void Awake()
    {
        // �����͸� �ε��� ��
        string a = "https://raw.githubusercontent.com/Unity2033/Unity-3D-Example/main/Assets/Class/Photon%20Server/Texture/Ice%20Kingdom.jpg";
        
        // [0] = ""
        // [1] = ""
        
        StartCoroutine(WebImageLoad(a));
    }

    private void Start()
    {
        // ĳ���� ��ġ ��
    }

    private IEnumerator WebImageLoad(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)// ����ó��
        {
            Debug.Log(www.error);
        }

        else
        {
            for (int i = 0; i < webImage.Length; i++)
            {
                //webImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture; 1 ����
                webImage[i].texture = ((DownloadHandlerTexture)www.downloadHandler).texture; // ������
            }
        }
    }


}
