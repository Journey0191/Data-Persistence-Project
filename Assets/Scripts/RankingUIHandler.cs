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
            CreateRankEntryTransform(rankEntry, rankListContainer, rankEntryTransformList);
        }
    }

    private void CreateRankEntryTransform(RankUserData rankEntry, Transform container, List<Transform> transformList)
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

        rankTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = rankEntry.userScore.ToString();

        rankTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = rankEntry.userName;

        transformList.Add(rankTransform);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
