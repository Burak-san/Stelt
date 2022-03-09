using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesKontrol : MonoBehaviour
{
    //statik deðer sistemde bir defa tanýmlandýktan sonra bir daha deðiþtirilemez
    private static GameObject instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // objenin sahneler arasýnda kaybolmamasýný saðlar.

        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {//gameobject çiftleþmesini engellemek için genel bir yöntemdir
            Destroy(gameObject);
        }
    }
}
