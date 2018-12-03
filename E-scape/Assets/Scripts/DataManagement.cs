using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManagement : MonoBehaviour {
    public static DataManagement manageData;
    public int highScore;

    void Awake()
    {
        if (manageData == null)
        {
            //does not delete data when scene loads
            DontDestroyOnLoad(gameObject);
            manageData = this;
        }
        //new instance of manageData replaces past instance
        else if (manageData != this)
            Destroy(gameObject);
    }

    public void SaveData()
    {//Data is saved
        BinaryFormatter BinForm = new BinaryFormatter(); //creates a binary formatter
        //data path that stays when application is updated
        FileStream file = File.Create(Application.persistentDataPath +"/gameInfo.dat"); //creates file
        gameData data = new gameData(); //creates container for data
        //sets highScore from DataManagement class to highscore from gameData class
        data.highscore = highScore;
        BinForm.Serialize(file, data); //serializes
        file.Close(); //closes file
    }

    public void LoadData()
    {//Data is loaded
        if(File.Exists (Application.persistentDataPath + "/gameInfo.dat")) //if file in condition exists
        {
            BinaryFormatter BinForm = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open); //opens file
            gameData data = (gameData)BinForm.Deserialize(file);
            file.Close();
            highScore = data.highscore;
        }
    }
}

[Serializable]
class gameData
{
    public int highscore;
}