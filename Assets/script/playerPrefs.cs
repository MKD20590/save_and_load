using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SaveLoad_playerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("username", "binusian");
        PlayerPrefs.SetString("password", "123");
        PlayerPrefs.SetString("lastLogin", DateTime.Now.ToString());
        PlayerPrefs.SetInt("exp", 9999);

        PlayerPrefs.GetString("username");
        PlayerPrefs.GetInt("exp");

        
        //class JSON
        GameData gameData = new GameData();
        gameData.username = "binus";
        gameData.password = "123";
        gameData.exp = 100;
        //bikin file JSON (returnnya string)
        string jsonString = JsonUtility.ToJson(gameData);
        print(jsonString);
        //balikin dr json ke tipe data di <> misal class GameData
        GameData newGameData = JsonUtility.FromJson<GameData>(jsonString);
        print(newGameData.exp);
        
        //save semua ke 1 playerpref
        PlayerPrefs.SetString("gameData", jsonString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
