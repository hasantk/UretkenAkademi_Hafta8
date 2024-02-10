using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopController : MonoBehaviour
{
    private Rigidbody rb;
    public Button btn;
    public float hiz = 1.8f;
    public Text can, zaman,durum;
    float zamanSayac� = 500;
    float canSayac� = 20;
    bool oyunDevam = true;
    bool oyunTamam = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (oyunDevam && !oyunTamam) 
        {
            zamanSayac� -= Time.deltaTime;
            zaman.text = (int)zamanSayac� + "";
        }
        else if(!oyunTamam)
        {
            durum.text = "Oyun Tamamlanamad�";
            btn.gameObject.SetActive(true);
        }
        if (canSayac� < 0 || zamanSayac� < 0) 
        {
            oyunDevam = false;
        }
    }

    private void FixedUpdate()
    {
        if (oyunDevam && !oyunTamam) 
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(dikey, 0, -yatay);
            rb.AddForce(kuvvet * hiz);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        string objIsmi = collision.gameObject.name;
        if (objIsmi.Equals("Bitis")) 
        {
            //print("Oyunu Kazand�n !!!!!");
            oyunTamam = true;
            durum.text = "Oyun Tamamland� Tebrikler";
            btn.gameObject.SetActive(true);
        }
        else if (!objIsmi.Equals("Zemin") && !objIsmi.Equals("Dama") && !objIsmi.Equals("Baslangic"))
        {
            canSayac� -= 1;
            can.text = canSayac� + "";
            if (canSayac�==0)
            {
                oyunDevam=false;
            }
        }
    }
}
