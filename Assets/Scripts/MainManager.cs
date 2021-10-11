using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    public GameObject LeaderBoardText;
    private bool m_Started = false;
    private int m_Points; 
    
    private bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        /*HighScoreText.text = "Best: " + 
        DataManager.Instance.Leaderboard.OrderBy(name => name.Value).Last().Key + ": " + 
        DataManager.Instance.Leaderboard.OrderBy(name => name.Value).Last().Value.ToString();
        */
        HighScoreText.text = "Best: " + 
        DataManager.Instance.TopPlayerNames[0] + ": " +
        DataManager.Instance.TopPlayerScores[0].ToString();

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            //Reload scene when pressing spacebar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UpdateLeaderboard();
                DataManager.Instance.SaveLeaderboard();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);      
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                UpdateLeaderboard();
                DataManager.Instance.SaveLeaderboard();
                SceneManager.LoadScene(2);    
            }

            /*
            if (m_Points > DataManager.Instance.Leaderboard.Last().Key) 
            {
                DataManager.Instance.HighestScore = m_Points;
                DataManager.Instance.HighScorePlayerName = DataManager.Instance.PlayerName;
            }*/
        }
    }

    // update the leaderboard according to this session's performance
    void UpdateLeaderboard()
    {
        for (int i = 0; i < DataManager.Instance.TopPlayerScores.Length; i++)
        {

            // if the player scored higher up than someone else on the leaderboard
            if (m_Points > DataManager.Instance.TopPlayerScores[i])
            {
                // shift the leaderboard positions one place down from index i
                ShiftLeaderboards(DataManager.Instance.TopPlayerScores,
                DataManager.Instance.TopPlayerNames, i);

                DataManager.Instance.TopPlayerNames[i] = DataManager.Instance.CurrentPlayerName;
                DataManager.Instance.TopPlayerScores[i] = m_Points;

                return; // gets out of the loop 
            }
        }
    }

    void ShiftLeaderboards(int[] topPlayerScores, string[] topPlayerNames, int index)
    {
        for (int i = topPlayerNames.Length - 1; i > index+1; i--)
        {
            topPlayerNames[i] = topPlayerNames[i-1];
            topPlayerScores[i] = topPlayerScores[i-1];
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        LeaderBoardText.SetActive(true);
    }
}
