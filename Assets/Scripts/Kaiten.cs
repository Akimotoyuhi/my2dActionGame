using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaiten : MonoBehaviour
{
    /// <summary> 移動速度 </summary>
    [SerializeField] private float m_speed = 10f;
    /// <summary> 円の大きさ </summary>
    [SerializeField] private float m_radius = 1f;
    /// <summary> 円の中心となるオブジェクトの位置 </summary>
    [SerializeField] private Transform m_targetPos;
    private float m_x = 0;
    private float m_y = 0;
    private Vector2 m_defPos;
    private float m_timer = 0f;

    void Start()
    {
        m_defPos = new Vector2(m_targetPos.position.x, m_targetPos.position.y);
    }

    void Update()
    {
        m_timer += Time.deltaTime;
        m_x = m_radius * Mathf.Sin(Time.time * m_speed);
        m_y = m_radius * Mathf.Cos(Time.time * m_speed);
        transform.position = new Vector2(m_x + m_defPos.x, m_y + m_defPos.y);
    }
}
