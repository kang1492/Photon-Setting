using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCount : MonoBehaviour
{
    //                  매개변수 입력하는곳에 적으면 됨
    public void Selected(int count)
    {
        switch(count)
        {
            case 0: Data.count = 0;
                break;
            case 1:
                Data.count = 1;
                break;
            case 2:
                Data.count = 2;
                break;

        }




    }


}
