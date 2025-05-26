using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad_fileStream : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        save();
        load();
    }

    public void save()
    {
        //nnt filenya namanya data.txt, directorynya dicariin otomatis
        string path = Application.persistentDataPath + "data.txt";
        FileStream filestream = new FileStream(path, FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(filestream);

        GameData gameData = new GameData();
        gameData.username = "binus";
        gameData.password = "test123";
        gameData.lastLogin = DateTime.Now.ToString();
        gameData.exp = 99;
        
        //wajib di close abis dipake biar gk makan memori
        string jsonString = JsonUtility.ToJson(gameData);
        writer.Write(jsonString);
        writer.Close();
    }
    public void load()
    {
        string path = Application.persistentDataPath + "data.txt";
        FileStream filestreamRead = new FileStream(path, FileMode.Open);
        StreamReader reader = new StreamReader(filestreamRead);
        print(reader.ReadToEnd());
        reader.Close();
    }
}
