using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public Button startButton;
    public TMP_InputField playerName;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void StartGame()
    {
        SceneManager.LoadScene(1);
        DataManager.Instance.CurrentPlayerName = playerName.text;

        // if the leaderboard doesn't contain your name, make an entry for it
        if (!DataManager.Instance.Leaderboard.ContainsKey(playerName.text))
        {
            DataManager.Instance.Leaderboard.Add(playerName.text, 0); // adds new entry with a default 0 score
        }
    }
}
