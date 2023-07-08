using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string UserName;
    public int BestScore;
    public string BestScoreUserName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public int BestScore;
        public string BestScoreUserName;
    }

    public void SaveBestScoreData()
    {
        SaveData data = new SaveData();
        data.BestScore = BestScore;
        data.BestScoreUserName = BestScoreUserName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/data-persistence-project-savefile.json", json);
    }

    public void LoadBestScoreData()
    {
        string path = Application.persistentDataPath + "/data-persistence-project-savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScore = data.BestScore;
            BestScoreUserName = data.BestScoreUserName;
        }
    }
}
