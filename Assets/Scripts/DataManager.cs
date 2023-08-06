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
    }

    public void SaveBestScoreData()
    {
        SortRankList(RankUserDataList);

        SaveData data = new SaveData();
        data.BestScore = BestScore;
        data.BestScoreUserName = BestScoreUserName;
        data.RankUserDataList = RankUserDataList;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/savefile.json", json);
    }

    public void LoadBestScoreData()
    {
        string path = Application.dataPath + "/savefile.json";
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
