using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;
    public string m_UserName;
    public string m_BestScoreUserName;
    public int m_BestScore;

    private List<RankUserData> m_RankUserDataList;

    private bool m_GameOver = false;

    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
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

        GetData();
        RefreshBestScore();
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"[Score] {m_Points}";

        if (m_Points > m_BestScore)
        {
            m_BestScore = m_Points;
            m_BestScoreUserName = m_UserName;
            RefreshBestScore();
        }

        for (int i = m_RankUserDataList.Count - 1; i >= 0; i--)
        {
            if (m_Points > m_RankUserDataList[i].userScore)
            {
                m_RankUserDataList[i].userName = m_UserName;
                m_RankUserDataList[i].userScore = m_Points;
                break;
            }
        }
    }

    void GetData()
    {
        m_UserName = DataManager.Instance.UserName;
        m_BestScore = DataManager.Instance.BestScore;
        m_BestScoreUserName = DataManager.Instance.BestScoreUserName;
        m_RankUserDataList = DataManager.Instance.RankUserDataList;
    }

    void RefreshBestScore()
    {
        BestScoreText.text = $"[Best Score] {m_BestScoreUserName} : {m_BestScore}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        DataManager.Instance.BestScore = m_BestScore;
        DataManager.Instance.BestScoreUserName = m_BestScoreUserName;
        
        DataManager.Instance.SaveBestScoreData();
        GameOverText.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
