using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUIHandler : MonoBehaviour
{
    private Transform settingContainer;
    private Transform settingListContainer;
    private Transform settingListTemplate;
    private List<SettingColorData> settingColorList;
    private List<Transform> settingColorTransformList;

    private void Awake()
    {
        settingContainer = transform.Find("SettingContainer");
        settingListContainer = settingContainer.Find("SettingListContainer");
        settingListTemplate = settingListContainer.Find("SettingListTemplate");

        settingListTemplate.gameObject.SetActive(false);

        settingColorList = DataManager.Instance.SettingColorDataList;

        settingColorTransformList = new List<Transform>();

        foreach (SettingColorData settingColor in settingColorList)
        {
            CreateSettingColorTransform(settingColor, settingListContainer, settingColorTransformList);
        }
    }

    private void CreateSettingColorTransform(SettingColorData settingColor, Transform container, List<Transform> transformList)
    {
        Transform settingTransform = Instantiate(settingListTemplate, container);
        settingTransform.gameObject.SetActive(true);

        settingTransform.Find("ObjectNameText").GetComponent<TextMeshProUGUI>().text = settingColor.objectName;

        settingTransform.Find("ObjectColorButton").GetComponent<Image>().color= settingColor.objectColor;

        transformList.Add(settingTransform);
    }

    public void onColorPickerEvent()
    {
        foreach (Transform settingTransform in settingColorTransformList)
        {
            foreach (SettingColorData settingColor in settingColorList)
            {
                SetColor(settingColor, settingTransform);
            }
        }
    }

    public void SetColor(SettingColorData settingColor, Transform container)
    {
        if (settingColor.objectName == container.Find("ObjectNameText").gameObject.GetComponent<TextMeshProUGUI>().text)
        {
            settingColor.objectColor = container.Find("ObjectColorButton").gameObject.GetComponent<Image>().color;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveSetting()
    {
        DataManager.Instance.SaveSettingData();
    }
}
