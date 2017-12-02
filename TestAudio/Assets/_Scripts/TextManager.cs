using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Data;
using Mono.Data.Sqlite;


public class TextManager : MonoBehaviour
{

    public Text ScoreText;
    public Text HightComboText;
    public Text PerfectText;
    public Text GoodText;
    public Text MissText;
    public Text CompletedText;
    public static float ctime = 0f;
    public GameObject Mus;
    public String[] SongName;

    public String Gra = "";
    PlayG Re;
    void Start()
    {
        Mus.SetActive(true);
        ShowData();
    }

    void Update()
    {
        if (PlayG.sh == 1)
        {
            ShowData();
            Mus.SetActive(true);
        }

    }

    void ShowData()
    {
        PlayG.sh = 0;
        PlayG.star = 0;
        ScoreText.text = MainScore.Score.ToString();
        HightComboText.text = MainScore.HightCombo.ToString() + " / " + MainScore.TotalCount.ToString();
        PerfectText.text = MainScore.Perfect.ToString();
        GoodText.text = MainScore.Good.ToString();
        MissText.text = MainScore.Miss.ToString();

        if (MainScore.Perfect == MainScore.TotalCount) { CompletedText.color = Color.yellow; CompletedText.text = "SS"; Gra = "SS"; }
        else if (MainScore.Miss == 0) { CompletedText.color = Color.red; CompletedText.text = "S"; Gra = "S"; }
        else if (MainScore.Miss <= 3) { CompletedText.color = Color.blue; CompletedText.text = "A"; Gra = "A"; }
        else if (MainScore.Miss <= 6) { CompletedText.color = Color.green; CompletedText.text = "B"; Gra = "B"; }
        else if (MainScore.Miss <= 10) { CompletedText.color = Color.cyan; CompletedText.text = "C"; Gra = "C"; }
        else { CompletedText.color = Color.gray; CompletedText.text = "F"; Gra = "F"; }

        Database db;//資料庫物件


        string databaseName = "Touch_Db";//資料庫名稱

        string tableName = "momo";//資料庫內的資料表名稱
        db = new Database(databaseName);
        try
        {
            db.insertInto(tableName, new string[] {"" + MainScore.Score + "","'" + Gra + "'", "'" + SongName[PlayG.MusicNo] + "'" });//資料庫的字串資料必須使用單引號框起來'Yang' 

        }
        catch(Exception error){
      
            Debug.Log("Error with " + error.ToString());

        }


        try { db.closeDatabaseConnecting(); db.releaseDatabaseAllResources(); }
        catch (Exception error)
        {
            Debug.Log("Error with " + error.ToString());
        }

    }

}