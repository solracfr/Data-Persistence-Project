using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string[] TopPlayerNames = new string[29];
    public int[] TopPlayerScores = new int[29];
    public string CurrentPlayerName;
    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; // refers to itself as the only (static) instance of this class; no need to make a reference to it
        DontDestroyOnLoad(gameObject);

        LoadLeaderboard();
    }

    [System.Serializable] // required for JsonUtility -- will only transform things to JSON if tagged as Serializable
    class SaveData // note that this is a class
    {
        public string[] PlayerNames; // might not be needed if only implementing highest scores
        public int[] HighestPlayerScores;
    }

    // convert leaderboard dictionary to arrays for keys and values and save for later
    public void SaveLeaderboard()
    {
        SaveData data = new SaveData();
        data.PlayerNames = new string[TopPlayerNames.Length];
        data.HighestPlayerScores =  new int[TopPlayerScores.Length];

        for (int i = 0; i < TopPlayerNames.Length; i++)
        {
            data.PlayerNames[i] = TopPlayerNames[i];
            data.HighestPlayerScores[i] = TopPlayerScores[i];
        }

        Debug.Log($"Data Saved: {data.PlayerNames}  {data.HighestPlayerScores}");

        /*
        int i = 0;
        foreach(KeyValuePair<string,int> player in Leaderboard)
        {
            data.PlayerNames[i] = player.Key;
            data.HighestPlayerScores[i] = player.Value;
        }
        */

        string json = JsonUtility.ToJson(data); // convert class SaveData instance data to a json

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); //TODO: STUDY PATHS LATER
    }


    // load leaderboard values onto the dictionary
    public void LoadLeaderboard()
    {
        string path = Application.persistentDataPath +  "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            for(int i = 0; i < data.PlayerNames.Length; i++)
            {
                //Leaderboard.Add(data.PlayerNames[i], data.HighestPlayerScores[i]);
                TopPlayerNames[i] = data.PlayerNames[i];
                TopPlayerScores[i] = data.HighestPlayerScores[i];
            }
            
            Debug.Log("Save Loaded");
        }
        else
        {
            Debug.Log("No save data implemented! Creating new save.");
        }
    }
}
