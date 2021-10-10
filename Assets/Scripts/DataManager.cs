using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    //implement dictionary of high scores
    public SortedDictionary<string, int> Leaderboard = new SortedDictionary<string, int>();
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

        LoadOverallBestScore();
    }

    [System.Serializable] // required for JsonUtility -- will only transform things to JSON if tagged as Serializable
    class SaveData // note that this is a class
    {
        //public string PlayerName; // might not be needed if only implementing highest scores
        public string HighScorePlayerName;
        public int HighestScore; 
    }

    // TODO: implement a history for various players
    public void SaveOverallBestScore()
    {
        SaveData data = new SaveData();
        //data.HighScorePlayerName = HighScorePlayerName;
        //data.HighestScore = HighestScore;

        string json = JsonUtility.ToJson(data); // convert class SaveData instance data to a json

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); //TODO: STUDY PATHS LATER
    }


    // TODO: implement a history for various players
    public void LoadOverallBestScore()
    {
        string path = Application.persistentDataPath +  "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            CurrentPlayerName = data.HighScorePlayerName; 
            //HighestScore = data.HighestScore;
        }
    }
}
