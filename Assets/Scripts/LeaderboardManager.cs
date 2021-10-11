using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderboardEntry;
    public GameObject canvas;
    [SerializeField] float verticalSpaceAvailable = 445f; // vertical real estate for leaderboard
    [SerializeField] float initialYPos = -180f;
    [SerializeField] float spaceBetweenEntries = 15f;

    public TextMeshProUGUI[] texts;
    // Start is called before the first frame update
    void Start()
    {   
        //max number of entries we'll have on the leaderboard
        int numEntries = (int)Mathf.Floor(verticalSpaceAvailable/spaceBetweenEntries);

        // create each entry GameObject and positon it accordingly within the canvas
        for (int i = 0; i < numEntries; i++)
        {
            string playerName = DataManager.Instance.TopPlayerNames[i];
            int playerScore = DataManager.Instance.TopPlayerScores[i];

            Vector3 spawnPosition = new Vector3(canvas.transform.position.x,
                                    canvas.transform.position.y + initialYPos - (spaceBetweenEntries * i),
                                    canvas.transform.position.z);

            TextMeshProUGUI[] fields = Instantiate(leaderboardEntry, spawnPosition,
                        leaderboardEntry.transform.rotation, canvas.transform).GetComponentsInChildren<TextMeshProUGUI>();

            fields[0].text = (i+1).ToString();
            fields[1].text = playerName;
            fields[2].text = playerScore.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(1);
        }
    }
}
