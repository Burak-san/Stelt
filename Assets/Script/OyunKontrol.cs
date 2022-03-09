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
    public TextMeshProUGUI Sayac;
    //tmp_text

    //SAYAÇ
    public float ToplamZaman;
    float dakika;
    float saniye;
    bool zamanlayici;
    
    
    
    void Start()
    {
        ilkSecimDegeri = 0;
        zamanlayici = true;
    }

    private void Update()
    {
        if (zamanlayici && ToplamZaman > 1)
        {
            //geri sayım
            ToplamZaman -= Time.deltaTime;

            dakika = Mathf.FloorToInt(ToplamZaman / 60); //1
            saniye = Mathf.FloorToInt(ToplamZaman % 60); //2

            //Sayac.text = Mathf.FloorToInt(ToplamZaman).ToString();
            Sayac.text = string.Format("{0:00}:{1:00}", dakika, saniye);
        }
        else
        {
            zamanlayici = false;
            GameOver();
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
            yield return new WaitForSeconds(1);

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
