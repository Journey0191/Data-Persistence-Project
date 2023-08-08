using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    
    public int PointValue;

    private Color m_Color1;
    private Color m_Color2;
    private Color m_Color3;
    private Color m_ColorDefault;

    private void Awake()
    {
        m_Color1 = DataManager.Instance.SettingColorDataList[0].objectColor;
        m_Color2 = DataManager.Instance.SettingColorDataList[1].objectColor;
        m_Color3 = DataManager.Instance.SettingColorDataList[2].objectColor;
        m_ColorDefault = DataManager.Instance.BrickColorDefault;
    }

    void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (PointValue)
        {
            case 1 :
                block.SetColor("_BaseColor", m_Color1);
                break;
            case 2:
                block.SetColor("_BaseColor", m_Color2);
                break;
            case 5:
                block.SetColor("_BaseColor", m_Color3);
                break;
            default:
                block.SetColor("_BaseColor", m_ColorDefault);
                break;
        }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        onDestroyed.Invoke(PointValue);
        
        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.2f);
    }
}
