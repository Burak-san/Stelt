using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesKontrol : MonoBehaviour
{
    //statik de�er sistemde bir defa tan�mland�ktan sonra bir daha de�i�tirilemez
    private static GameObject instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // objenin sahneler aras�nda kaybolmamas�n� sa�lar.

        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {//gameobject �iftle�mesini engellemek i�in genel bir y�ntemdir
            Destroy(gameObject);
        }
    }
}
