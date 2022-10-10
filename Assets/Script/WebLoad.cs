using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;// 이미지 불러오기


public class WebLoad : MonoBehaviour
{
    // 웹에서 이미지를 불러올 때는 Raw Image를 사용해야 합니다.
    [SerializeField] RawImage [] webImage; // 이미지 여러개 받기
    //[SerializeField] RawImage webImage; 이미지 1개 받기

    [SerializeField] string[] imageAddress; // 10-10

    void Awake()
    {
        // 데이터를 로드할 때
        //string a = "https://raw.githubusercontent.com/Unity2033/Unity-3D-Example/main/Assets/Class/Photon%20Server/Texture/Ice%20Kingdom.jpg";

        imageAddress[0] = "https://raw.githubusercontent.com/Unity2033/Unity-3D-Example/main/Assets/Class/Photon%20Server/Texture/Ice%20Kingdom.jpg";
        imageAddress[1] = "https://github.com/Unity2033/Unity-3D-Example/blob/main/Assets/Class/Photon%20Server/Texture/Leader%20Board%20Panel.png?raw=true";
        imageAddress[2] = "https://github.com/Unity2033/Unity-3D-Example/blob/main/Assets/Class/Photon%20Server/Texture/Leader%20Board%20Panel.png?raw=true";
        imageAddress[3] = "https://github.com/Unity2033/Unity-3D-Example/blob/main/Assets/Class/Photon%20Server/Texture/Leader%20Board%20Panel.png?raw=true";

        // [0] = ""
        // [1] = ""

        for (int i = 0; i < imageAddress.Length; i++)
        {
            StartCoroutine(WebImageLoad(imageAddress[i]));
        }
    }

    private void Start()
    {
        // 캐릭터 위치 값
    }

    private IEnumerator WebImageLoad(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)// 예외처리
        {
            Debug.Log(www.error);
        }

        else
        {
            for (int i = 0; i < webImage.Length; i++)
            {
                //webImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture; 1 개만
                webImage[i].texture = ((DownloadHandlerTexture)www.downloadHandler).texture; // 여러개
            }
        }
    }


}
