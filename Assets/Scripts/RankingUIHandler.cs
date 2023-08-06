using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Linq;

public class RankingUIHandler : MonoBehaviour
{
    private Transform rankContainer;
    private Transform rankListContainer;
    private Transform rankListTemplate;
    private List<RankUserData> rankEntryList;
    private List<Transform> rankEntryTransformList;

    private void Awake()
    {
        rankContainer = transform.Find("RankContainer");
        rankListContainer = rankContainer.Find("RankListContainer");
        rankListTemplate = rankListContainer.Find("RankListTemplate");

        rankListTemplate.gameObject.SetActive(false);

        rankEntryList = DataManager.Instance.RankUserDataList;
        
        rankEntryTransformList = new List<Transform>();

        foreach (RankUserData rankEntry in rankEntryList)
        {
            createRankEntryTransform(rankEntry, rankListContainer, rankEntryTransformList);
        }
    }

    private void Start()
    {
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void createRankEntryTransform(RankUserData rankEntry, Transform container, List<Transform> transformList)
    {
        Transform rankTransform = Instantiate(rankListTemplate, container);
        rankTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            default:
                rankString = rank + "TH";
                break;
            case 1:
                rankString = rank + "ST";
                break;
            case 2:
                rankString = rank + "ND";
                break;
            case 3:
                rankString = rank + "RD";
                break;
        }

        rankTransform.Find("RankText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = rankEntry.userScore;

        rankTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = rankEntry.userName;

        rankTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(rankTransform);
    }
}
