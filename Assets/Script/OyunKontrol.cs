using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour
{
    //GENEL AYARLAR
    int ilkSecimDegeri;
    int anlikbasari;
    public int hedefbasari;

    GameObject secilenButon;
    GameObject butonunKendisi;
    
    public Sprite defaultSprite;
    public AudioSource[] sesler;
    public GameObject[] Butonlar;
    public GameObject[] OyunSonuPaneller;
    public TextMeshProUGUI Sayac;//tmp_text
    public Slider ZamanSlider;
    public Button StopButton;
    public Sprite Castle;

    //SAYAÇ
    public float ToplamZaman;
    //float dakika;
    //float saniye;
    bool zamanlayici;
    float gecenzaman;

    public GameObject Grid;
    public GameObject Havuz;
    bool OlusturmaDurumu;
    int OlusturmaSayisi;
    int ToplamElemanSayisi;
    void Start()
    {
        ilkSecimDegeri = 0;
        zamanlayici = true;
        gecenzaman = 0;
        OlusturmaDurumu = true;
        OlusturmaSayisi = 0;
        ToplamElemanSayisi = Havuz.transform.childCount;
        ZamanSlider.value = ToplamZaman - gecenzaman;
        ZamanSlider.maxValue = ToplamZaman;
        StopButton.GetComponent<Button>().enabled = false;

        /* obje oluşturma alternatif
         * Gameobject obje = Instantiate(eklenecekobje);
         * RectTransform rt = obje.GetComponent<RectTransform>();
         * rt.localScale = new Vector3(.358f,.544f,1f);
         * obje.transform.SetParent(Grid.transform);*/
        StartCoroutine(Olustur());
    }
    private void Update()
    {

        if (OlusturmaDurumu == false)
        {
            if (zamanlayici && gecenzaman != ToplamZaman)
            {
                //geri sayım
                gecenzaman += Time.deltaTime;
                ZamanSlider.value = ToplamZaman - gecenzaman;
                if (ZamanSlider.value == 0)
                {
                    sesler[4].Play();
                    zamanlayici = false;
                    GameOver();
                }


                //sayaç için bir alternatif
                /*dakika = Mathf.FloorToInt(ToplamZaman / 60); //1
                saniye = Mathf.FloorToInt(ToplamZaman % 60); //2

                //Sayac.text = Mathf.FloorToInt(ToplamZaman).ToString();
                Sayac.text = string.Format("{0:00}:{1:00}", dakika, saniye);*/
            }
            /*else
            {
                zamanlayici = false;
                GameOver();
            }*/
        }


    }

    IEnumerator Olustur()
    {

        Butonlarindurumu(false);
        while (OlusturmaDurumu)
        {
            yield return new WaitForSeconds(0.03f);
            int rastgelesayi = Random.Range(0, Havuz.transform.childCount - 1);
            if (Havuz.transform.GetChild(rastgelesayi).gameObject!=null)
            {
                Havuz.transform.GetChild(rastgelesayi).transform.SetParent(Grid.transform);
                //havuz objesinin rastgelesayi'nci çocuğunu grid objesinin çocuğu yaptım
                OlusturmaSayisi++;
                if (OlusturmaSayisi == ToplamElemanSayisi)
                {
                    OlusturmaDurumu = false;
                    //Destroy(Havuz.gameObject);
                    Butonlarindurumu(true);
                    StopButton.GetComponent<Button>().enabled = true;
                }
            }

        }

    }
    public void OyunuDurdur()
    {
        OyunSonuPaneller[2].SetActive(true);
        Time.timeScale = 0;
    }
    public void OyunaDevamEt()
    {
        OyunSonuPaneller[2].SetActive(false);
        Time.timeScale = 1;
    }

    public void TekrarOyna()
    {
        

        foreach (var item in Butonlar)
        {
            if (item != null)
            {
                item.GetComponent<Image>().sprite = Castle;
                item.GetComponent<Image>().enabled = true;
                item.GetComponent<Button>().enabled = true;
                OyunSonuPaneller[0].SetActive(false);
                OyunSonuPaneller[1].SetActive(false);
                OyunSonuPaneller[2].SetActive(false);
                ilkSecimDegeri = 0;
                zamanlayici = true;
                gecenzaman = 0;
                OlusturmaDurumu = false;
                ToplamElemanSayisi = Havuz.transform.childCount;
                ZamanSlider.value = ToplamZaman - gecenzaman;
                ZamanSlider.maxValue = ToplamZaman;
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
            }
        }

        
    }
    public void YeniLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void GameOver()
    {
        OyunSonuPaneller[0].SetActive(true);
    }
    void Win()
    {
        sesler[3].Play();
        OyunSonuPaneller[1].SetActive(true);
    }
    public void AnaMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ObjeVer(GameObject objem) 
    {
        butonunKendisi = objem;

        butonunKendisi.GetComponent<Image>().sprite = butonunKendisi.GetComponentInChildren<SpriteRenderer>().sprite;
        butonunKendisi.GetComponent<Image>().raycastTarget = false;
        
    }
    void Butonlarindurumu(bool durum)
    {
        foreach (var item in Butonlar)
        {
            if (item!=null)
            {
                item.GetComponent<Image>().raycastTarget = durum;

            }            
        }

    }  
    public void ButonTikladi(int deger)
    {

        Kontrol(deger);
        
    }   
    void Kontrol(int gelendeger)
    {

        if (ilkSecimDegeri == 0)
        {
            ilkSecimDegeri = gelendeger;
            secilenButon = butonunKendisi;
            sesler[2].Play();
        }
        else
        {
            StartCoroutine(kontroletbakalim(gelendeger));
        }

    }
    IEnumerator kontroletbakalim(int gelendeger)
    {
        Butonlarindurumu(false);
        

        if (ilkSecimDegeri == gelendeger)
        {
            anlikbasari++;
            sesler[0].Play();
            yield return new WaitForSeconds(0.5f);

            secilenButon.GetComponent<Image>().enabled = false;
            secilenButon.GetComponent<Button>().enabled = false;

            butonunKendisi.GetComponent<Image>().enabled = false;
            butonunKendisi.GetComponent<Button>().enabled = false;

            /*Destroy(secilenButon.gameObject);
            Destroy(butonunKendisi.gameObject);*/
            ilkSecimDegeri = 0;
            secilenButon = null;
            Butonlarindurumu(true);

            if(hedefbasari == anlikbasari)
            {
                Win();
                zamanlayici = false;
            }
        }
        else
        {
          
            sesler[1].Play();
            yield return new WaitForSeconds(1);

            secilenButon.GetComponent<Image>().sprite = defaultSprite;
            butonunKendisi.GetComponent<Image>().sprite = defaultSprite;

            ilkSecimDegeri = 0;
            secilenButon = null;
            Butonlarindurumu(true);


        }

    }
}
