using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string UserName;

    public int BestScore;
    public string BestScoreUserName = "-";

    private int rankNum = 10;
    public List<RankUserData> RankUserDataList;

    public List<SettingColorData> SettingColorDataList;

    public Color BrickColor1 = Color.green;
    public Color BrickColor2 = Color.yellow;
    public Color BrickColor3 = Color.blue;
    public Color BrickColorDefault = Color.red;
    public Color BallColor = Color.white;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        RankUserDataList = new List<RankUserData>(rankNum);

        for (int i = 0; i < rankNum; i++)
        {
            RankUserDataList.Add(new RankUserData { });
        }

        SettingColorDataList = new List<SettingColorData>();
        SettingColorDataList.Add(new SettingColorData { objectName = "Brick1", objectColor = BrickColor1 });
        SettingColorDataList.Add(new SettingColorData { objectName = "Brick2", objectColor = BrickColor2 });
        SettingColorDataList.Add(new SettingColorData { objectName = "Brick3", objectColor = BrickColor3 });
        SettingColorDataList.Add(new SettingColorData { objectName = "Ball", objectColor = BallColor });
    }

    private void Start()
    {

    }

    [System.Serializable]
    class SaveData
    {
        public int BestScore;
        public string BestScoreUserName;

        public List<RankUserData> RankUserDataList = new List<RankUserData>();

        public List<SettingColorData> SettingColorDataList = new List<SettingColorData>();
    }

    public void SaveBestScoreData()
    {
        SortRankList(RankUserDataList);

        SaveData data = new SaveData();
        data.BestScore = BestScore;
        data.BestScoreUserName = BestScoreUserName;
        data.RankUserDataList = RankUserDataList;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/ranking_savefile.json", json);
    }

    public void LoadBestScoreData()
    {
        string path = Application.dataPath + "/ranking_savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScore = data.BestScore;
            BestScoreUserName = data.BestScoreUserName;

            if (data.RankUserDataList.Count != 0)
            {
                RankUserDataList = data.RankUserDataList.ToList();
            }
        }
    }

    public void SaveSettingData()
    {
        SaveData data = new SaveData();
        data.SettingColorDataList = SettingColorDataList;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/setting_savefile.json", json);
    }

    public void LoadSettingData()
    {
        string path = Application.dataPath + "/setting_savefile.json";
        if (File.Exists (path))
        {
            string json = File.ReadAllText (path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (data.SettingColorDataList.Count != 0)
            {
                SettingColorDataList = data.SettingColorDataList.ToList();
            }
        }
    }

    private void SortRankList(List<RankUserData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                if (list[i].userScore < list[j].userScore)
                {
                    RankUserData tmp = list[i];
                    list[i] = list[j];
                    list[j] = tmp;
                }
            }
        }
    }
}
