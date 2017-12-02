using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RollNode : MonoBehaviour
{

    public Image Node;

    private bool pointin{ set; get; }
    private float RollTimer { set; get; }
    private float Timer3 { set; get; }
    private bool enableR { set; get; }
    private bool ReSetRTimer { set; get; }

    public Text text;
    public Image Row;
    public Animator TB;
    public Animator Arrowani;
    public AudioSource AudioR;
   
    public string Anima;
    Vector3 posT;

    public int con = 0;

    void Start()
    {
        posT = Row.transform.position;
        pointin = false;
        Timer3 = 0.0f;
        RollTimer = 0.0f;
        enableR = false;
        ReSetRTimer = false;
    }

    void Update()
    {

        if (!ReSetRTimer)
        {
            ReSetRTimer = true;
            RollTimer = 0.0f;
            pointin = false;
            Timer3 = 0.0f;
            enableR = false;   
        }

        if (MainScore.Combo > MainScore.HightCombo) { MainScore.HightCombo = MainScore.Combo; }

        if (Node.enabled)
        {
            if (RollTimer == 0.0f)
            {
                Timer3 = 0.0f;
                Arrowani.Play("arrow");
            }

            if (PlayG.m == 0)
            {
                if (enableR)
                {
                    Arrowani.speed = 1;
                    TB.speed = 1;
                    enableR = false;
                }

                RollTimer += Time.deltaTime;

                if (RollTimer >= 2.8f)
                {
                    MainScore.Combo = 0;
                    text.text = "Miss";
                    text.gameObject.SetActive(true);
                    MainScore.Miss++;                 
                    PlayG.scorebase = 100;
                    ReSetV();
                }

                if (pointin && RollTimer < 2.8f)
                {
                    AnimatorStateInfo info = TB.GetCurrentAnimatorStateInfo(0);
                    if (Timer3 <= 0.8f && info.normalizedTime >= 1.0f)
                    { 
                        MainScore.Combo++;
                        if (MainScore.Combo % 50 == 0 && MainScore.Combo != 0) { PlayG.scorebase += 100; }                       
                        AudioR.PlayOneShot(AudioR.clip);
                        text.text = "Perfect";
                        text.gameObject.SetActive(true);
                        MainScore.Perfect++;
                        MainScore.Score += PlayG.scorebase;                       
                        ReSetV();
                    }
                    else if (Timer3 <= 1.5f && info.normalizedTime >= 1.0f)
                    {
                        MainScore.Combo++;
                        if (MainScore.Combo % 50 == 0 && MainScore.Combo != 0) { PlayG.scorebase += 100; }                       
                        AudioR.PlayOneShot(AudioR.clip);
                        text.text = "Good";
                        text.gameObject.SetActive(true);
                        MainScore.Good++;
                        MainScore.Score += (PlayG.scorebase - 50);                      
                        ReSetV();
                    }
                    
                }
            }
            else
            {
                enableR = true;
                TB.speed = 0;
                Arrowani.speed = 0;
            }          
        }
        else
        {
            RollTimer = 0.0f;           
            Timer3 = 0.0f;
        }

    }

    public void OnPointerEnter()
    {
        pointin = true;
        TB.Play(Anima);
        TB.enabled = true;
        Timer3 = RollTimer;          
    }

    public void OnPointerExit()
    {
        if (pointin)
        {
            MainScore.Combo = 0;
            text.text = "Miss";
            text.gameObject.SetActive(true);           
            MainScore.Miss++;
            PlayG.scorebase = 100;
            ReSetV();
        }
    }


    void ReSetV()
    {       
        RollTimer = 0.0f;
        Timer3 = 0.0f;
        pointin = false;
        enableR = false;
        ReSetRTimer = false;
        Row.transform.position = posT;
        TB.enabled = false;
        Node.gameObject.SetActive(false);
        Node.enabled = false;
    }
 
}
