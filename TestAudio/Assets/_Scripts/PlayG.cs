using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class PlayG : MonoBehaviour
{
    #region -- Object --
    public GameObject[] Go; // node
    public Image[] Go1;     // node
    public float[] BPM;    // each song's BPM
    public AudioClip[] music;   // each Song
    public Sprite[] BKd;        // Song's Background
    public Text[] textShCount;

    public GameObject p1;   // this panel
    public GameObject p2;   // Score panel
    public GameObject p3;   // Songmenu Panel
    public GameObject p4;   // Wait Panel
    public Image BK;        // Background
    public Text ScoreText;
    public Text ComboText;
    public Text HighLightText;
    public VideoPlayer ViPlay;
    public RawImage PlayTexture;
    public AudioSource playerM;
    public Animator countdown;

    #endregion

    #region -- private --
    private int d { set; get; }       // record before node number
    private int n { set; get; }       // Combo'semaphore
    private bool invokeS { set; get; }       // 'semaphore
    private int invokeNo { set; get; }       // 'semaphore

    private float No1 { set; get; }
    private float No2 { set; get; }
    private float No3 { set; get; }


    private float chartDelayTimer { set; get; }
    private float chartTimer { set; get; }
    private float comboTimer { set; get; }

    private bool isMusicStarted { set; get; }
    private bool isMusicEnding { set; get; }

    #endregion

    #region -- public --
    public static int MusicNo { set; get; }   // record song's No
    public static int SpeedCho { set; get; }   // record speed's No
    public static int m { set; get; }              // pause's semaphore
    public static int sh { set; get; }             // Show's semaphore
    public static int star { set; get; }               // InstanUI's semaphore 
    public static int scorebase { set; get; }
    public int cc = 0;

    #endregion


    #region -- Music1's node --
    int[] e1 = new int[]{
         0,  1,  2,  3,  4,  5,  6,  7,  8,  9,     // 10
        10, 11, 12, 13, 14, 15,  2,  4,  6,  8,     // 20
        10, 12, 14,  1,  3,  5,  7,  9, 11, 13,     // 30
        15,  4,  9,  5,  0, 10,  3,  6,  7, 16,     // 40
         1,  8,  4, 12,  2,  0, 13, 11,  7,  9,     // 50
        10,  5,  2,  1,  6, 15,  4, 12,  3, 14,     // 60
         0,  7,  9,  2, 11,  1, 12, 14,  4, 13,     // 70
         7,  5, 15,  9, 12,  1,  6,  3,  0, 14,     // 80
         2,  1,  7,  6, 14, 13,  9, 12, 11, 10,     // 90
         9,  8,  7,  6, 15,  5,  3,  4,  2,  1,     // 100
         0, 10, 11, 12, 13,  8,  9, 10,  4,  1,     // 110
         3,  5,  7,  9,  8,  6,  4,  2,  0, 16,     // 120
         1,  8,  4, 12,  2,  0, 13, 11,  7,  9,     // 130
        10,  5,  2,  1,  6, 15,  4, 12,  3, 14,     // 140
         2,  1,  8,  7, 14, 13,  0,  2, 10,  9,     // 150
        13, 12,  2, 11, 16, 10,  8,  0,  7, 14,     // 160
         4,  3, 10,  9, 16,  1,  8, 14,  4, 13,     // 170
         7,  5,  6,  9, 12,  1, 15, 11,  2, 14,     // 180
         6,  5, 12, 11,  0,  3,  0, 16, 11, 10,     // 190
         9,  5,  0,  1,  3,  8, 13,  4, 15,  7,     // 200          
        11, 12,  0,  1,  6,  8, 13,  4, 15, 10,     // 210
         7,  5,  6,  9, 12,  1, 15, 13,  2, 14,     // 220          
        10, 12, 14,  0,  3,  5,  7,  9, 11, 13,     // 230
        15,  4,  1,  5,  0, 12,  3,  6,  7, 16,     // 240
         1,  8,  4, 12,  2,  0, 13, 11,  7,  9,     // 250
         3,  5, 10,  9,  8,  6,  7,  2,  0, 16,     // 260
         1,  8,  4, 12,  7,  0, 11, 13,  2,  9      // 270
    };

    #endregion

    #region -- Music2's node --
    int[] e2 = new int[]{10, 14,  0, 11,  8,  1, 5, 12,  4,  7,  2, 15, 13,  3,  5, 10, 12,  0, 9, 14,
                          3,  6,  8,  4, 16, 2,  1,  7, 10,  9, 13, 12,  2, 11,  0, 10,  5, 15,  3, 14,
                         10,  9,  8, 16,  2,  0, 11, 10,  4,  9, 3,  5,  2,  1,  6, 15,  4, 12,  3, 14,
                         10,  0,  8,  2, 16,  1, 11, 14,  4, 13,  7,  5,  2,  9, 12,  1, 15,  3,  2, 14,
                          0,  1,  7,  6, 14, 13,  0, 16, 11, 10,  9,  5,  0,  2,  3, 8, 13,  4, 15,  7,
                         10,  9,  8, 16,  2,  0, 11, 10,  4,  9, 7,  5,  2,  1,  6, 15,  4, 12,  3, 14,
                         10,  0,  9,  2, 16,  1, 8, 14,  4, 13,  7,  5,  2,  9, 12,  1, 15,  3,  4, 14,
                          3,  6,  8,  4, 11, 8,  1,  7, 10,  9, 13, 12,  2, 11,  0, 10,  5,  0,  3, 14,
                         10,  0,  6,  2,  8,  1, 3, 14,  4, 13,  7,  5,  2,  9, 12,  1, 11,  3,  2, 14,
                          0,  1,  2,  6, 14, 13,  0, 16, 11, 10,  9,  5,  0, 2,  3, 8, 13,  4, 15,  7,
                         10, 14,  0, 11,  8
                        };
    #endregion

    #region -- Music3's node --
    int[] e3 = new int[]{
         9,  5,  0,  1,  3,  8, 13,  4, 15, 10,     // 10
         7,  5,  6,  9, 12,  1, 15, 13,  2, 14,     // 20          
        10, 12, 14,  0,  3,  5,  7,  9, 11, 13,     // 30
        15,  4,  1,  5,  0, 12,  3,  6,  7, 16,     // 40
         1,  8,  4, 12,  2,  0, 13, 11,  7,  9,     // 50
        10,  5,  2,  1,  6, 15,  4, 12,  3, 14,     // 60
        13, 12,  5, 11, 16, 10,  8,  0,  7, 14,     // 70
         2,  1,  7,  6, 14, 13,  9, 12, 11, 10,     // 80
         7,  5, 15,  9, 12,  1,  6,  3,  0, 14,     // 90
         9,  8,  7, 13, 15,  5,  3,  4,  2,  1,     // 100
         0, 10, 11, 12, 13,  8,  9, 10,  4,  1,     // 110
         3,  5,  7,  9,  8,  6,  4,  2,  0, 16,     // 120
         1,  8,  4, 12,  2,  0, 13, 11,  7,  9,     // 130
         4,  3, 10,  9, 16,  1,  8, 14,  4, 13,     // 140
         0,  7,  9,  2, 11,  3, 12, 14,  4, 13,     // 150       
         2,  1,  8,  7, 14, 13,  0,  2, 11,  9,     // 160
        10,  5,  2,  1,  6, 15,  4, 12,  3, 14,     // 170
        10, 11, 12, 13, 14, 15,  2,  4,  6,  8,     // 180
         6,  5, 12, 11,  0,  3,  8, 16, 11, 10,     // 190
         0,  1,  2,  3,  4,  5,  6,  7,  8,  9,     // 200 
         7,  5, 15,  9, 12,  1,  6,  3,  0, 14,     // 210
         2,  1,  7,  6, 14, 13,  9, 12, 11, 10,     // 220
         9,  8,  7,  6, 15,  5,  3,  4,  2,  1,     // 230
         0, 10, 11, 12, 13,  8,  9, 10,  4,  1,     // 240
         3,  5,  7,  9,  8,  6,  4,  2,  0, 16,     // 250
         1,  8,  4, 12,  2,  0, 13, 11,  7,  9      // 260
        };

    #endregion

    #region -- Music4's node --
    int[] e4 = new int[]{0, 1, 16,  6,  5,  4,  3, 12,  2,  8,  9, 10, 11,  6, 8,  5, 13,  7,  5, 14,
                         3, 6,  8,  4,  0,  2,  1,  7, 10,  9, 13, 12,  2, 11,  0, 13,  5, 15,  3, 14,
                        10, 9,  8,  1,  2,  0, 13, 16,  4,  9, 10,  5,  2,  1,  6, 15,  4, 12,  3, 14,
                        12, 0, 10,  2, 11,  1, 15, 14,  4, 13,  7,  5,  2,  9, 12,  1, 16,  3,  2, 14,
                         0, 1,  2,  6, 14, 13,  0, 16, 11, 10,  9,  5,  0,  1,  3, 8, 13,  4, 15,  7,
                        10, 9,  8,  6,  2,  0, 11, 10,  4,  9,  3,  5,  2,  1,  6, 13,  4, 12,  3, 14,
                        10, 0,  9,  2, 11,  1, 8, 14,  4, 13,  7,  5,  2,  9, 12,  1, 15,  3,  2, 14,
                         3, 6,  8,  4, 11,  0,  1,  7, 10,  9, 13, 12,  2, 11,  0, 10, 16,  0,  3, 14,
                        10, 0,  3,  2,  8,  1, 8, 14,  4, 13,  7,  5,  6,  9, 12,  1, 15,  3,  2, 14,
                         0, 1,  2,  6, 14, 13,  0, 16, 11, 10,  9,  5,  0, 1,  3, 8, 13,  4, 15,  7                      
                        };
    #endregion

    #region -- Music5's node --
    int[] e5 = new int[]{
         9,  8,  7,  6,  5,  4,  3,  2,  1,  0,     // 10
         8,  6,  4,  2, 15, 14, 12, 13, 11, 10,     // 20
         1,  3,  5,  7,  9, 11, 13, 16,  2,  9,     // 30
         5,  4,  0,  3,  1, 10, 11,  6,  7, 14,     // 40
        13,  8,  4, 12,  2,  0, 15, 11,  7,  9,     // 50
        10,  5,  2,  1,  8, 13,  4, 12,  3, 11,     // 60
         0,  6,  7,  2, 10,  1, 12, 14,  4, 13,     // 70
         7,  5, 15,  9, 12,  0,  6,  3,  1, 14,     // 80
         2,  8,  7,  6, 14, 13,  9, 12, 11, 10,     // 90
         9,  8,  7,  6, 16,  5,  3,  4,  2,  1,     // 100
         0, 10, 11, 12,  7,  8,  9, 10,  3,  1,     // 110
         3,  5, 10,  9,  8,  6,  7,  2,  0, 16,     // 120
         1,  8,  4, 12,  7,  0, 11, 13,  2,  9,     // 130
        10,  5,  7,  1,  6, 16,  4, 12,  3, 14,     // 140
         2,  1,  8,  7, 13, 14,  0,  2, 10,  9,     // 150
        13, 12,  2, 11, 16, 10,  8,  0,  7, 14,     // 160
         4,  3, 10,  9, 11,  0,  8, 11,  4, 13,     // 170
         7,  5,  2,  9, 12,  1, 15, 11,  3, 14,     // 180
         6,  5, 12, 11,  0,  3,  0, 16, 11, 10,     // 190
         9,  2,  0,  1,  3,  8, 13,  4, 10, 12,     // 200 
        11, 14,  6,  0,  3,  5,  7,  9, 11, 13,     // 210
        15,  4,  1,  5,  0, 12,  3,  6,  7, 16,     // 220
         1,  8,  4, 12,  2,  0, 13, 11,  7,  9,     // 230
        10,  5,  2,  1,  6, 15,  4, 12,  3, 14,     // 240
        13, 12,  5, 11, 16, 10,  8,  0,  7, 14,     // 250
         2,  1,  7,  6, 14, 13,  9, 12, 11, 10      // 260
        };

    #endregion

    /*  #region -- Music2's node time --
    float[] tm2 = new float[]{0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f,            // 10s
                              0.4f, 0.4f, 0.4f, 0.4f, 0.5f, 0.4f, 0.8f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.5f, 0.5f, 0.4f, 0.4f, 0.4f, 0.4f, 0.8f, 0.6f,            // 8.3s
                              0.6f, 0.6f, 0.6f, 0.4f, 0.8f, 0.6f, 0.5f, 0.3f, 0.3f, 0.3f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.5f,            // 10.3s
                              0.5f, 0.4f, 0.4f, 0.4f, 0.8f, 0.7f, 0.6f, 0.5f, 0.5f, 0.4f, 0.4f, 0.4f, 0.4f, 0.5f, 0.5f, 0.5f, 0.4f, 0.8f, 0.6f, 0.5f,            // 10.2s
                              0.4f, 0.4f, 0.4f, 0.5f, 0.5f, 0.5f, 0.4f, 0.4f, 0.8f, 0.6f, 0.5f, 0.5f, 0.4f, 0.4f, 0.4f, 0.5f, 0.4f, 0.4f, 0.4f, 0.9f,            // 9.7s
                              0.5f, 0.3f, 0.3f, 0.3f, 0.8f, 0.6f, 0.6f, 0.6f, 0.6f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.6f, 0.8f, 0.6f, 0.6f, 0.6f,            // 10.8s
                              0.4f, 0.4f, 0.4f, 0.4f, 0.8f, 0.7f, 0.5f, 0.5f, 0.5f, 0.4f, 0.4f, 0.3f, 0.3f, 0.4f, 0.4f, 0.4f, 0.4f, 0.8f, 0.5f, 0.5f,            // 9.4s
                              0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.4f, 0.4f, 0.4f, 0.4f, 0.3f, 0.3f, 0.3f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f,            // 7.1s
                              0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.4f, 0.4f, 0.4f, 0.4f, 0.3f, 0.3f, 0.3f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f,            // 7.1s
                              0.3f, 0.3f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.5f, 0.8f, 0.6f, 0.4f, 0.4f, 0.4f, 0.3f, 0.4f, 0.4f, 0.5f, 0.6f, 0.6f, 0.9f,            // 10.4s
                              0.6f, 0.6f, 0.6f, 0.6f, 0.6f
                            };
    #endregion*/

    /*  #region -- Music4's node time --
      float[] tm4 = new float[]{ 1.0f, 0.7f, 0.8f, 0.8f, 0.8f, 1.0f, 1.0f, 1.0f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f,                       // 8s
                               0.7f, 0.7f, 0.7f, 0.7f, 0.6f, 0.6f, 0.5f, 0.5f, 0.5f, 0.2f, 0.2f, 0.2f, 0.4f, 0.4f, 0.4f, 0.5f, 0.5f, 0.5f, 0.8f, 0.6f,            // 10.2s
                               0.5f, 0.4f, 0.4f, 0.5f, 0.5f, 0.4f, 0.4f, 0.2f, 0.2f, 0.2f, 0.4f, 0.4f, 0.5f, 0.5f, 0.5f, 0.5f, 0.8f, 0.6f, 0.5f, 0.5f,            // 8.8s
                               0.4f, 0.4f, 0.4f, 0.4f, 0.5f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.4f, 0.4f, 0.4f, 0.5f, 0.5f, 0.5f, 0.5f, 0.8f, 0.6f, 0.5f,            // 10.2s
                               0.4f, 0.4f, 0.4f, 0.5f, 0.5f, 0.5f, 0.4f, 0.4f, 0.8f, 0.6f, 0.5f, 0.3f, 0.3f, 0.4f, 0.4f, 0.6f, 0.3f, 0.3f, 0.3f, 0.3f,            // 8.6s
                               0.3f, 0.3f, 0.3f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f,            // 6.6s
                               0.3f, 0.3f, 0.3f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 1.0f, 0.8f, 1.0f, 0.9f, 0.6f,                  // 8.2s
                               0.2f, 0.2f, 0.2f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.4f, 0.4f, 0.4f, 0.4f, 0.5f, 0.5f, 0.5f, 0.5f, 0.8f, 0.6f, 0.5f,            // 9.1s
                               0.4f, 0.4f, 0.4f, 0.4f, 0.5f, 0.5f, 0.5f, 0.8f, 0.5f, 0.4f, 0.4f, 0.4f, 0.4f, 0.5f, 0.5f, 0.5f, 0.5f, 0.8f, 0.6f, 0.5f,            // 10.2s
                               0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f
                              };
      #endregion*/


    float[] sp = new float[] { 2.0f, 1.5f, 1.0f };


    public void PauseB()
    {
        playerM.Pause();                           // Pause
        m = 1;                                     // Pause's semaphore set = 1
        ViPlay.Pause();
        invokeNo = 0;
        invokeS = false;
        CancelInvoke("StartGame");
        CancelInvoke("GenerateNode");
    }

    public void ContinueB()
    {
        StartCoroutine(ContinueW());            // wait for 4 seconds to continue
        p4.SetActive(true);
        countdown.Play("countdown");
    }

    public void Retry()
    {

        playerM.gameObject.SetActive(false);    // turn off music
        ReSet();                                // reset all scores and isStart or isEnd   and time's value  
        //     StartCoroutine(Play());                 // wait for 4 seconds to start
        p4.SetActive(true);
        countdown.Play("countdown");
        CancelInvoke("StartGame");
        CancelInvoke("GenerateNode");
    }

    public void BackToMenu()
    {

        ReSet();                               // reset all scores and isStart or isEnd   and time's value   
        playerM.gameObject.SetActive(false);   // turn off music
        p1.SetActive(false);                   // turn off this panel
        p3.SetActive(true);                    // turn on songmenu panel
    }


    void Update()
    {
    //    Debug.Log(isMusicStarted + "          " + m);
        if (chartDelayTimer == 0.0f && No1 == 0.0f)
        {

            InvokeRepeating("Init", 0.0f, 1.0f);         
        }

        #region -- ScoreText & ComboText Set --
        ScoreText.text = MainScore.Score.ToString();    // Scroetext
        if (ScoreText.text.Length == 2)
        {
            ScoreText.text = "00000" + MainScore.Score.ToString();
        }
        else if (ScoreText.text.Length == 3)
        {
            ScoreText.text = "0000" + MainScore.Score.ToString();
        }
        else if (ScoreText.text.Length == 4)
        {
            ScoreText.text = "000" + MainScore.Score.ToString();
        }
        else if (ScoreText.text.Length == 5)
        {
            ScoreText.text = "00" + MainScore.Score.ToString();
        }
        else if (ScoreText.text.Length == 6)
        {
            ScoreText.text = "0" + MainScore.Score.ToString();
        }
        else
        {
            ScoreText.text = "000000" + MainScore.Score.ToString();
        }
        ComboText.text = "x " + MainScore.Combo.ToString();     // Combotext 
        #endregion
    }

    void Init()
    {
        Debug.Log("you");
        ReSet();
        BK.sprite = BKd[MusicNo];
        playerM.clip = music[MusicNo];
        if (MusicNo == 4)
        {
            PlayTexture.gameObject.SetActive(true);
            ViPlay.gameObject.SetActive(true);
        }
        No1 = (60 / BPM[MusicNo]) * (sp[SpeedCho] - 0.2f);
        No2 = (60 / BPM[MusicNo]) * (sp[SpeedCho] - 0.1f);
        No3 = (60 / BPM[MusicNo]) * sp[SpeedCho];

        StartCoroutine(Play());
        CancelInvoke("Init");

    }

    void StartGame()
    {
        
        if (m == 0)
        {
            chartDelayTimer += 1.0f;                      // record song's time            

            #region -- HighLightCombo Set --
            if (n > 0 && HighLightText.IsActive())              // record Combo Show's time
            {
                comboTimer += 0.01f;
            }
            if ((MainScore.Combo % 50) == 0 && MainScore.Combo > 0 && n == 0)   // each 50 up show combo
            {
                comboTimer += 0.01f;
                HighLightText.text = MainScore.Combo.ToString();
                HighLightText.gameObject.SetActive(true);
                n += 1;
            }
            else if (n > 0 && (MainScore.Combo % 50) != 0 && !HighLightText.IsActive())     // Combo's semaphore
            {
                n = 0;
            }

            if (comboTimer >= 0.3)            //  Combo Show's time over
            {
                HighLightText.gameObject.SetActive(false);
                comboTimer = 0.0f;
            }
            #endregion


            #region - Run -
            if (isMusicStarted && !isMusicEnding)
            {
                Debug.Log(invokeNo + "        " + invokeS);
                if (MusicNo != 1 && MusicNo != 3)
                {
                    if (AudioPeer._freqBand[1] > 3 && AudioPeer._freqBand[4] > 3 && AudioPeer._freqBand[5] > 3 && AudioPeer._freqBand[6] > 3)
                    {
                        if (invokeS && (invokeNo != 1))
                        {
                            invokeS = false; CancelInvoke("GenerateNode");
                        }
                        if (!invokeS)
                        {
                            //  Debug.Log("1");
                            InvokeRepeating("GenerateNode", 0.0f, No1);
                            invokeS = true;
                            invokeNo = 1;
                        }
                    }
                    else if (AudioPeer._freqBand[1] > 4 && AudioPeer._freqBand[6] > 4)
                    {
                        if (invokeS && (invokeNo != 2))
                        {
                            invokeS = false; CancelInvoke("GenerateNode");
                        }
                        if (!invokeS)
                        {
                            //  Debug.Log("2");
                            InvokeRepeating("GenerateNode", 0.0f, No2);
                            invokeS = true;
                            invokeNo = 2;
                        }

                    }
                    else if (AudioPeer._freqBand[1] > 1)
                    {
                        if (invokeS && (invokeNo != 3))
                        {
                            invokeS = false; CancelInvoke("GenerateNode");
                        }
                        if (!invokeS)
                        {
                            //  Debug.Log("3");
                            InvokeRepeating("GenerateNode", 0.0f, No3);
                            invokeS = true;
                            invokeNo = 3;
                        }

                    }

                }
                /*   else if (MusicNo == 1)
                   {
                       chartTimer += 1.0f;                          //  record rhythm's time
                       if (cc < e1.Length)
                       {
                           if (chartTimer >= tm2[cc])
                           {
                               if (!Go1[e2[cc]].enabled)
                               {
                                   MainScore.TotalCount++;
                                   Go[e2[cc]].SetActive(true);
                                   Go1[e2[cc]].enabled = true;
                                   textShCount[e2[cc]].text = MainScore.TotalCount.ToString();
                               }
                               cc++;
                               chartTimer = 0.0f;
                           }
                       }
                   }
                   else if (MusicNo == 3)
                   {
                       chartTimer += 0.1f;                          //  record rhythm's time
                       if (cc < e4.Length)
                       {
                           if (chartTimer >= tm4[cc])
                           {
                               if (!Go1[e4[cc]].enabled)
                               {
                                   MainScore.TotalCount++;
                                   Go[e4[cc]].SetActive(true);
                                   Go1[e4[cc]].enabled = true;
                                   textShCount[e4[cc]].text = MainScore.TotalCount.ToString();
                               }
                               cc++;
                               chartTimer = 0.0f;

                           }
                       }
                   }
                   */
            }
           
            #endregion


            #region -- MusicEnd --
            if (music[MusicNo].length <= chartDelayTimer) // before end
            {
                isMusicEnding = true;
                CancelInvoke("GenerateNode");
            }

            if (music[MusicNo].length + 4 <= chartDelayTimer)       // End
            {
                playerM.gameObject.SetActive(false);               // turn off music 
                if (MainScore.Combo > MainScore.HightCombo) { MainScore.HightCombo = MainScore.Combo; }     // record HightCombo

                chartDelayTimer = 0.0f;
                sh = 1;
                p1.SetActive(false);                            // turn off this panel
                p2.SetActive(true);                             // turn on Score panel
                CancelInvoke("StartGame");
            }

            #endregion
        }

    }

    void GenerateNode()
    {
        if (m == 0 && !isMusicEnding)
        {
            /*         Random.InitState(System.Guid.NewGuid().GetHashCode());
                     int i = 0;
                     MainScore.TotalCount++;                         // record Totalcount       
                     if (MainScore.TotalCount % 34 == 0) { i = Go.Length - 2; }
                     else if (MainScore.TotalCount % 21 == 0) { i = Go.Length - 1; }
                     else { i = Random.Range(0, Go.Length - 2); }               // random
                     for (int x = 0; (Go1[i].enabled || i == d); x++)    // iS Node is enabled
                     {
                         i = (i + 2) % (Go1.Length - 2);

                         if (i == d)                                 // equal before  /  + 2
                         {
                             i = (i + 2) % (Go1.Length - 2);
                         }
                     }
                     if (!Go1[i].enabled && d != i)                        // no  / show
                     {
                         Go[i].SetActive(true);
                         Go1[i].enabled = true;
                         textShCount[i].text = MainScore.TotalCount.ToString();
                         d = i;                                  // record before node's number
                     }
          */

            if (MusicNo == 0 && cc < e1.Length)
            {
                if (!Go1[e1[cc]].enabled)
                {
                    MainScore.TotalCount++;
                    Go[e1[cc]].SetActive(true);
                    Go1[e1[cc]].enabled = true;
                    textShCount[e1[cc]].text = MainScore.TotalCount.ToString();
                }
                cc++;
            }
            else if (MusicNo == 1 && cc < e2.Length)
            {
                if (!Go1[e2[cc]].enabled)
                {
                    MainScore.TotalCount++;
                    Go[e2[cc]].SetActive(true);
                    Go1[e2[cc]].enabled = true;
                    textShCount[e2[cc]].text = MainScore.TotalCount.ToString();
                }
                cc++;
            }
            else if (MusicNo == 2 && cc < e3.Length)
            {
                if (!Go1[e3[cc]].enabled)
                {
                    MainScore.TotalCount++;
                    Go[e3[cc]].SetActive(true);
                    Go1[e3[cc]].enabled = true;
                    textShCount[e3[cc]].text = MainScore.TotalCount.ToString();
                }
                cc++;
            }
            else if (MusicNo == 3 && cc < e4.Length)
            {
                if (!Go1[e4[cc]].enabled)
                {
                    MainScore.TotalCount++;
                    Go[e4[cc]].SetActive(true);
                    Go1[e4[cc]].enabled = true;
                    textShCount[e4[cc]].text = MainScore.TotalCount.ToString();
                }
                cc++;
            }
            else if (MusicNo == 4 && cc < e5.Length)
            {
                if (!Go1[e5[cc]].enabled)
                {
                    MainScore.TotalCount++;
                    Go[e5[cc]].SetActive(true);
                    Go1[e5[cc]].enabled = true;
                    textShCount[e5[cc]].text = MainScore.TotalCount.ToString();
                }
                cc++;
            }

        }
    }

    void ReSet()
    {
        Debug.Log("?");
        CancelInvoke("StartGame");
        CancelInvoke("GenerateNode");
        MainScore.Score = 0;
        MainScore.Combo = 0;
        MainScore.Good = 0;
        MainScore.Miss = 0;
        MainScore.Perfect = 0;
        MainScore.HightCombo = 0;
        MainScore.TotalCount = 0;
        MainScore.TotalScores = 0;

        comboTimer = 0.0f;
        chartDelayTimer = 0.0f;
        chartTimer = 0.0f;
        scorebase = 100;
        isMusicEnding = false;
        isMusicStarted = false;
        PlayTexture.gameObject.SetActive(false);
        ViPlay.gameObject.SetActive(false);

        for (int a = 0; a < Go1.Length; a++)         // turn off nodes
        {
            Go1[a].enabled = false;
            Go[a].SetActive(false);
        }

        m = 0;                                      // Pause's semaphore
        sh = 0;
        cc = 0;
        star = 0;
        invokeS = false;
        invokeNo = 0;
        No1 = 0.0f;
        No2 = 0.0f;
        No3 = 0.0f;

        ScoreText.text = "000000" + MainScore.Score.ToString();
        ComboText.text = "x " + MainScore.Combo.ToString();     // Combotext
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(4);
        p4.SetActive(false);
        star = 1;
        m = 0;

        if (MusicNo == 4)
        {
            ViPlay.Play();
        
        }
        Debug.Log("P");
        playerM.gameObject.SetActive(true);
        isMusicStarted = true;
        InvokeRepeating("StartGame", 0.0f, 1.0f);

    }

    IEnumerator ContinueW()
    {
        yield return new WaitForSeconds(4);
        p4.SetActive(false);
        playerM.Play();
        m = 0;
        Debug.Log("C");
        ViPlay.Play();
        InvokeRepeating("StartGame", 0.0f, 1.0f);
    }

}
