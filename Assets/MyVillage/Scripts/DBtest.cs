using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.Networking;



public class DBtest : MonoBehaviour
{
    void Start() {
        string conn = "URI=file:" + Application.dataPath + "/DBTest.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT USERNUMBER, ID, NICKNAME " + "FROM Test";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read()) {
            int number = reader.GetInt32(0);
            int id = reader.GetInt32(1);
            int name = reader.GetInt32(2);

            Debug.Log("Usernumber= " + number + "  Id = " + id + "  Name = " + name);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

}
