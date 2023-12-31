using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]

public class StartMenuUIHandler : MonoBehaviour
{
    public Text BestScoreText;
    public TMP_InputField UserNameInputField;

    private void Start()
    {
        DataManager.Instance.LoadBestScoreData();
        DataManager.Instance.LoadSettingData();
        RefreshBestScore();
    }

    public void StartNew()
    {
        DataManager.Instance.UserName = UserNameInputField.text;
        SceneManager.LoadScene(1);
    }

    public void LoadRankingScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadSettingingScene()
    {
        SceneManager.LoadScene(3);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    void RefreshBestScore()
    {
        int bestScore = DataManager.Instance.BestScore;
        string bestScoreUserName = DataManager.Instance.BestScoreUserName;
        BestScoreText.text = $"[Best Score] {bestScoreUserName} : {bestScore}";
    }

}
