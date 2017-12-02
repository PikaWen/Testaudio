using System;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Dbtest : MonoBehaviour
{

    Database db;//資料庫物件

    string databaseName = "Touch_Db";//資料庫名稱

    string tableName = "momo";//資料庫內的資料表名稱

    SqliteDataReader reader;//搜尋資料表的資料



    /* Unity預設的函數執行順序為：Awake -> OnEablen -> Start -> FixedUpdate -> Update -> LateUpdate -> OnGUI -> (結束時繼續往下走，否則回到FixedUpdate) -> OnDisable -> OnDestroy */

    void Start ()
    {
        Input.multiTouchEnabled = true;
        db = new Database(databaseName);
        if (db.isTableExists(tableName) == false)//確認是否已有指定的資料表，若沒有則創造該資料表同時插入資料
        {
            db.createTable(tableName, new string[] { "Score", "Completed", "SongName", "Speed" }, new string[] { "INTEGER", "TEXT", "TEXT", "INTEGER" });//TEXT為SQLite的字串型態，INTEGER為SQLite的整數型態

        }
        try { db.closeDatabaseConnecting(); db.releaseDatabaseAllResources(); }
        catch (Exception error)
        {
            Debug.Log("Error with " + error.ToString());
        }
    }

}

