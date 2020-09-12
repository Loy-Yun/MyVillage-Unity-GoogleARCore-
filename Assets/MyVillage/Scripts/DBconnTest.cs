using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.Networking;



public class DBconnTest : MonoBehaviour
{
    public GameObject DB = null;

    // Start is called before the first frame update
    void Start()
    {
        DB.GetComponent<DBManager>().UpdateData("HOUSE_T", "BIGHOUSE", 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
