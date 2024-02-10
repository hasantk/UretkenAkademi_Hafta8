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
    float zamanSayacý = 500;
    float canSayacý = 20;
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
            zamanSayacý -= Time.deltaTime;
            zaman.text = (int)zamanSayacý + "";
        }
        else if(!oyunTamam)
        {
            durum.text = "Oyun Tamamlanamadý";
            btn.gameObject.SetActive(true);
        }
        if (canSayacý < 0 || zamanSayacý < 0) 
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
            //print("Oyunu Kazandýn !!!!!");
            oyunTamam = true;
            durum.text = "Oyun Tamamlandý Tebrikler";
            btn.gameObject.SetActive(true);
        }
        else if (!objIsmi.Equals("Zemin") && !objIsmi.Equals("Dama") && !objIsmi.Equals("Baslangic"))
        {
            canSayacý -= 1;
            can.text = canSayacý + "";
            if (canSayacý==0)
            {
                oyunDevam=false;
            }
        }
    }
}
