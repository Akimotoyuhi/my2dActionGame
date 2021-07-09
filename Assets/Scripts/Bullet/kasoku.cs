using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kasoku : NormalBullet
{
    [SerializeField] private float m_speedTimer = 0;
    private float timer;
    private float speedPerSeconds;
    //private Rigidbody2D m_rb;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        speedPerSeconds = m_speedTimer / 60;
    }

    void Update()
    {
        //timer += Time.deltaTime;
        //SpeedTimerで時間を指定し、現在の速度からn秒で最高速度に到達してほしい
        
        m_rb.velocity = new Vector2(m_rb.velocity.x + speedPerSeconds, m_rb.velocity.y + speedPerSeconds);
    }
}
