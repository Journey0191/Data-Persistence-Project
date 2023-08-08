using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private Color m_Color;

    private void Awake()
    {
        m_Color = DataManager.Instance.SettingColorDataList[3].objectColor;
    }

    void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock ball = new MaterialPropertyBlock();
        ball.SetColor("_BaseColor", m_Color);
        
        renderer.SetPropertyBlock(ball);

        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > 3.0f)
        {
            velocity = velocity.normalized * 3.0f;
        }

        m_Rigidbody.velocity = velocity;
    }
}
