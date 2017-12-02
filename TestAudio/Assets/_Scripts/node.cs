using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class node : MonoBehaviour
{


    public Image Node;
    private float Timer { set; get; }
    private bool enablesam { set; get; }
    private bool ReSetTimer { set; get; }

    public Text text;
    public Animator ani;
    public AudioSource AudioG;


    void Start()
    {
        Timer = 0.0f;
        enablesam = false;
        ReSetTimer = false;
    }

    void Update()
    {
        if(!ReSetTimer)
        {
            ReSetTimer = true;
            Timer = 0.0f;
        }
        if (MainScore.Combo > MainScore.HightCombo) { MainScore.HightCombo = MainScore.Combo; }

        if (Node.enabled)
        {
            if (Timer == 0.0f)
            {
                ani.Play("frame");
            }
            if (PlayG.m == 0)
            {

                if (enablesam)
                {
                    ani.speed = 1;
                    enablesam = false;
                }

                Timer += Time.deltaTime;

                if (Timer >= 1.0f)
                {
                    Timer = 0.0f;
                    MainScore.Combo = 0;
                    text.text = "Miss";
                    text.gameObject.SetActive(true);
                    MainScore.Miss++;
                    PlayG.scorebase = 100;
                    ReSetTimer = false;
                    gameObject.SetActive(false);
                    Node.enabled = false;
                    
                    //            CancelInvoke("StartNode");
                }
            }
            else
            {
                enablesam = true;
                ani.speed = 0;
                //           CancelInvoke("StartNode");
            }

        }
        else
        {
            Timer = 0.0f;
        }
        
    }



    public void OnPointerEnter()
    {

        if (PlayG.m == 0)
        {

            if (Timer < 0.5f && Timer >= 0.0f)
            {
                Timer = 0.0f;
                MainScore.Combo = 0;
                text.text = "Miss";
                text.gameObject.SetActive(true);
                MainScore.Miss++;
                PlayG.scorebase = 100;
                ReSetTimer = false;
                gameObject.SetActive(false);
                Node.enabled = false;
            }
            else if (Timer >= 0.5f && Timer < 0.7f)
            {
               
                Timer = 0.0f;
                MainScore.Combo++;
                if (MainScore.Combo % 50 == 0 && MainScore.Combo != 0) { PlayG.scorebase += 100; }            
                AudioG.PlayOneShot(AudioG.clip);
                text.text = "Good";
                text.gameObject.SetActive(true);
                MainScore.Good++;
                MainScore.Score += (PlayG.scorebase - 50);
                ReSetTimer = false;
                gameObject.SetActive(false);
                Node.enabled = false;
            }
            else if (Timer >= 0.7f)
            {

                Timer = 0.0f;
                MainScore.Combo++;
                if (MainScore.Combo % 50 == 0 && MainScore.Combo != 0) { PlayG.scorebase += 100; }
                AudioG.PlayOneShot(AudioG.clip);
                text.text = "Perfect";
                text.gameObject.SetActive(true);
                MainScore.Perfect++;
                MainScore.Score += PlayG.scorebase;
                ReSetTimer = false;
                gameObject.SetActive(false);
                Node.enabled = false;
            }
            else
            {
                Debug.Log("Error");
                Timer = 0.0f;
                text.text = "Error";
                text.gameObject.SetActive(true);
                ReSetTimer = false;
                gameObject.SetActive(false);
                Node.enabled = false;
            }

        }
    }


}