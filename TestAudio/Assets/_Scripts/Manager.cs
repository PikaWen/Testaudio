using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class Manager : MonoBehaviour {

    public GameObject MainP;
    public GameObject PlayP;
    public GameObject SpeedP;
    public GameObject SongP;
    public GameObject ScoreP;
    public GameObject TextP;
    public GameObject WaitP;

    public GameObject Mus;
    public Text textG;
    private int SNo { get; set; }
    private int SpeedN { get; set; }
    public String[] SongName;
    public Text[] Scores;
    public Text[] Grades;
    public Text[] Nums;
    public Text[] HightS;
    public Text[] HightG;

    Database db;//資料庫物件


    string databaseName = "Touch_Db";//資料庫名稱

    string tableName = "momo";//資料庫內的資料表名稱

   void Start()
    {
         db = new Database(databaseName);
            db.deleteAllData(tableName);

            try {  db.closeDatabaseConnecting(); db.releaseDatabaseAllResources(); }
            catch (Exception error)
             {
                 Debug.Log("Error with " + error.ToString());
             }
  
        MainP.SetActive(true);
        Mus.SetActive(true);
        PlayG.SpeedCho = 0;
        PlayG.MusicNo = 0;

    }

    public void Play()
    {
        Mus.SetActive(false);
        MainP.SetActive(false);
        PlayP.SetActive(true);
        HightScoreSet();
    }



    public void Score()
    {
        MainP.SetActive(false);
        ScoreP.SetActive(true);
        for (int z = 0; z < 3; z++)
        {
            Nums[z].text = " ";
            Scores[z].text = " ";
            Grades[z].text = " ";

        }
        textG.gameObject.SetActive(false);
    }

    public void ScoreToMain()
    {
        MainP.SetActive(true);
        ScoreP.SetActive(false);
        
    }

    public void TextToSongM()
    {
        TextP.SetActive(false);
        PlayP.SetActive(true);
        HightScoreSet();
        Mus.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }



    public void Show()
    {
        textG.gameObject.SetActive(false);
        for (int z = 0; z < 3; z++)
        {
            Nums[z].text = " ";
            Scores[z].text = " ";
            Grades[z].text = " ";

        }


        db = new Database(databaseName);

        SqliteDataReader reader;//搜尋資料表的資料
        try
        {

            reader = db.searchAccordData(tableName, "SongName", SongName[SNo], "order by Score DESC LIMIT 3");
            int[] data = db.readIntData(reader, "Score");

                if (data.Length == 0) { textG.gameObject.SetActive(true); }
                else
                {
                    for (int t = 0; t < data.Length; t++)
                    {
                        Nums[t].text = (t+1).ToString() + ".";
                        Scores[t].text =  data[t].ToString();
                    }
                }
                reader = db.searchAccordData(tableName, "SongName", SongName[SNo], "order by Score DESC LIMIT 3");
                string[] comp = db.readStringData(reader, "Completed");
                if (comp.Length != 0) 
                {
                    for (int t = 0; t < comp.Length; t++)
                    {
                        Grades[t].text = comp[t].ToString();
                        if (comp[t] == "SS") { Grades[t].color = Color.yellow; }
                        else if (comp[t] == "S") { Grades[t].color = Color.red; }
                        else if (comp[t] == "A") { Grades[t].color = Color.blue; }
                        else if (comp[t] == "B") { Grades[t].color = Color.green; }
                        else if (comp[t] == "C") { Grades[t].color = Color.black; }
                        else if (comp[t] == "F") { Grades[t].color = Color.white; }

                    }
                }
            
        }
        catch (Exception error)
        {
            Debug.Log("Error with " + error.ToString());

        }
        try {  db.closeDatabaseConnecting(); db.releaseDatabaseAllResources(); }
        catch (Exception error)
        {
            Debug.Log("Error with " + error.ToString());
        }

    }


    public void HightScoreSet()
    {

        db = new Database(databaseName);

        SqliteDataReader reader;//搜尋資料表的資料
        try
        {


            for (int i = 0; i < SongName.Length; i++)
            {
                reader = db.searchAccordData(tableName, "SongName", SongName[i], "order by Score DESC LIMIT 1");
                int[] data = db.readIntData(reader, "Score");

                if (data.Length != 0) {
                    HightS[i].text = "HightScore : " + data[0].ToString();                   
                }
                
                reader = db.searchAccordData(tableName, "SongName", SongName[i], "order by Score DESC LIMIT 1");
                string[] comp = db.readStringData(reader, "Completed");
                if (comp.Length != 0)
                {              
                        HightG[i].text = comp[0].ToString();
                        if (comp[0] == "SS") { HightG[i].color = Color.yellow; }
                        else if (comp[0] == "S") { HightG[i].color = Color.red; }
                        else if (comp[0] == "A") { HightG[i].color = Color.blue; }
                        else if (comp[0] == "B") { HightG[i].color = Color.green; }
                        else if (comp[0] == "C") { HightG[i].color = Color.black; }
                        else if (comp[0] == "F") { HightG[i].color = Color.white; }
                }
            }
        }
        catch (Exception error)
        {
            Debug.Log("Error with " + error.ToString());

        }
        try { db.closeDatabaseConnecting(); db.releaseDatabaseAllResources(); }
        catch (Exception error)
        {
            Debug.Log("Error with " + error.ToString());
        }
       
    }

    public void MusicNoSet(int a)
    {
        PlayG.MusicNo = a;
        SpeedP.SetActive(true);
    }

    public void SpeedSet(int a)
    {
        PlayG.SpeedCho = a;
        SpeedP.SetActive(false);
        PlayP.SetActive(false);
        SongP.SetActive(true);
        WaitP.SetActive(true);
    }

    public void SongNoSet(int a)
    {
        SNo = a;      
    }
    public void SongSpeed(int a)
    {
        SpeedN = a;
    }
}
