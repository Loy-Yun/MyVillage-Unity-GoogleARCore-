using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour {
    private string id = "player5_id";
    private string name = "player5_name";
    private int coin = 0;
    private int[] fruits = new int[] { 0, 0, 0, 0, 0 };  // [CARROT, TURNIP, PUMPKIN, APPLE, GRAPE]
    private int[] houses = new int[] { 1, 0 };  // [SMALLHOUSE, BIGHOUSE]
    private int[] trees = new int[] { 1, 0 };  // [APPLETREE, GRAPETREE]
    private int[] farms = new int[] { 1, 0, 0 };  // [CARROTFARM, TURNIPFARM, PUMPKINFARM]
    private int[] furnitures = new int[] { 0, 0, 0, 0 }; // [BED, TABLE, SOFA, CLOSET]
    private List<int[]> items = new List<int[]>();
    private string conn = "";
    private string[] tables = new string[] { "PLAYER_T", "FRUIT_T", "HOUSE_T", "FARM_T", "TREE_T", "FURNITURE_T" };

    // Start is called before the first frame update
    void Start()
    {
        items.Add(fruits);
        items.Add(houses);
        items.Add(farms);
        items.Add(trees);
        items.Add(furnitures);
        // app에서 아이디, 닉네임 받아오기      
                
        conn = "URI=file:" + Application.dataPath + "/MyVillageDB.db"; //Path to database.

        using (var connection = new SqliteConnection(conn)) {
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = "SELECT count(*) FROM PLAYER_T WHERE ID='"+@id+"'";
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count == 0) { // record exists
                command.ExecuteNonQuery();
                command.Dispose();
                InsertData(connection);
            }
            else { // record doesn't exist
                command.ExecuteNonQuery();
                command.Dispose();
                ReadData(connection);
            }
            connection.Close();
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void InsertData(dynamic connection) {
        for (int i = 0; i < tables.Length; i++) {
            if (i == 0) {
                using (var command = connection.CreateCommand()) {
                    command.CommandTimeout = 0;
                    command.CommandText = "INSERT INTO " + @tables[i] + "(ID,Name) VALUES (@ID, @Name)";
                    command.Parameters.Add(new SqliteParameter("@ID", id));
                    command.Parameters.Add(new SqliteParameter("@NAME", name));
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
            else {
                using (var command = connection.CreateCommand()) {
                    command.CommandTimeout = 0;
                    command.CommandText = "INSERT INTO " + @tables[i] + "(ID) VALUES (@ID)";
                    command.Parameters.Add(new SqliteParameter("@ID", id));
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
        }
    }
    
    private void ReadData(dynamic connection) {
        for (int i = 0; i < tables.Length; i++) {
            if (i == 0) {
                using (var command = connection.CreateCommand()) {
                    command.CommandTimeout = 0;
                    command.CommandText = "SELECT COIN " + "FROM " + @tables[i] + " WHERE ID='" + @id + "'";
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        coin = reader.GetInt32(0);
                        //Debug.Log("ID = " + id + " Coin = " + coin);
                    }
                    reader.Close();
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
            else {
                using (var command = connection.CreateCommand()) {
                    command.CommandTimeout = 0;
                    command.CommandText = "SELECT * " + "FROM " + @tables[i] + " WHERE ID='" + @id + "'";
                    IDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read()) {
                        id = reader.GetString(0);
                        //string s = "TABLE = " + tablenames[i] + " ID = " + id;
                        for (int j = 1; j <= items[i-1].Length; j++) {
                            items[i-1][j -1] = reader.GetInt32(j);
                            //s += " ITEMS[" + (j) + "] = " + items[i - 1][j - 1];
                        }                        
                        //Debug.Log(s);
                    }
                    reader.Close();
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
        }        
    }

    public void UpdateData(string tablename, string itemname, int index, int itemnum) {

        int findIndex = Array.IndexOf(tables, tablename);

        items[findIndex - 1][index] = itemnum;

        using (var connection = new SqliteConnection(conn)) {
            connection.Open();

            using (var command = connection.CreateCommand()) {
                command.CommandTimeout = 0;
                
                command.CommandText = "UPDATE " + @tablename + " SET " + @itemname + " = " + itemnum + " WHERE ID='" + @id + "'";
                command.ExecuteNonQuery();
                command.Dispose();
            }
            connection.Close();
        }           
    }

    public int PlayerData() {
        return coin;
    }

    public int[] FruitData() {
        return fruits;
    }

    public int[] HouseData() {
        return houses;
    }

    public int[] FarmData() {
        return farms;
    }

    public int[] TreeData() {
        return trees;
    }

    public int[] FurnitureData() {
        return furnitures;
    }
}
