using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesKontrol : MonoBehaviour
{
    //statik değer sistemde bir defa tanımlandıktan sonra bir daha değiştirilemez
    private static GameObject instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // objenin sahneler arasında kaybolmamasını sağlar.

        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {//gameobject çiftleşmesini engellemek için genel bir yöntemdir
            Destroy(gameObject);
        }
    }
}
