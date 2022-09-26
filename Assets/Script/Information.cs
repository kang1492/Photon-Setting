using Photon.Pun;
using UnityEngine.UI;

public class Information : MonoBehaviourPunCallbacks
{
    public Text roomData;

    // string name : �濡 ���� �̸�
    // int currentCount : �濡 ���� ������ �ο�
    // int MaxCount : �濡 �ִ�� ������ �� �ִ� �ο�
    public void SetInfo(string name, int currentCount, int MaxCount)
    {
        roomData.text = name + " ( " + currentCount + " / " + MaxCount + " ) ";
    }



}
