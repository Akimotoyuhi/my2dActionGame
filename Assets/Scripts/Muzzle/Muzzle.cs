﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    /// <summary>発射する弾の速度</summary>
    [SerializeField] public float m_bulletSpeed = 1f;
    /// <summary>弾を発射する間隔（秒）</summary>
    [SerializeField] public float m_fireInterval = 1f;
    /// <summary>扇状に弾を出す範囲(度)</summary>
    [SerializeField] public float m_angle = 45f;
    /// <summary>way数</summary>
    [SerializeField] public int m_wayNum = 3;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] public GameObject m_bulletPrefab = null;
    /// <summary>弾の色</summary>
    [SerializeField] Color m_color;
    [System.NonSerialized] public float m_timer;

    public void InstantiateAndColor(Vector2 v)
    {
        var t = Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);
        t.GetComponent<Rigidbody2D>().velocity = v;
        if (t.GetComponent<SpriteRenderer>())
        {
            t.GetComponent<SpriteRenderer>().color = m_color;
        }
    }

    /// <summary> 単発の自機狙い弾を撃つ </summary>
    public void Single()
    {
        GameObject player = GameObject.Find("Player");
        if (player)
        {
            // 一定間隔で弾を発射する
            m_timer += Time.deltaTime;
            if (m_timer > m_fireInterval)
            {
                m_timer = 0f;

                Vector2 v = player.transform.position - this.transform.position;
                v.Normalize();
                v *= m_bulletSpeed;
                InstantiateAndColor(v);
            }
        }
    }
    /// <summary>
    /// 単発の弾を撃つ
    /// </summary>
    /// <param name="v">射出方向</param>
    public void Single(Vector2 v)
    {
        // 一定間隔で弾を発射する
        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            m_timer = 0f;

            v.Normalize();
            v *= m_bulletSpeed;
            InstantiateAndColor(v);
        }
    }

    /// <summary>
    /// Angleで角度、WayNumでway数を指定して自機狙いのNway弾を撃つ
    /// </summary>
    public void Way()
    {
        GameObject player = GameObject.Find("Player");
        if (player)
        {
            m_timer += Time.deltaTime;
            if (m_timer > m_fireInterval)
            {
                m_timer = 0;
                for (int i = 0; i < m_wayNum; i++)
                {
                    Vector2 v = player.transform.position - this.transform.position;
                    v.Normalize();
                    v = Quaternion.Euler(0, 0, m_angle / (m_wayNum - 1) * i - m_angle / 2) * v;
                    v *= m_bulletSpeed;
                    InstantiateAndColor(v);
                }
            }
        }
    }
    /// <summary>
    /// Angleで角度、WayNumでway数を指定してNway弾を撃つ
    /// </summary>
    /// <param name="v">射出方向</param>
    public void Way(Vector2 v)
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            m_timer = 0;
            for (int i = 0; i < m_wayNum; i++)
            {
                v.Normalize();
                v = Quaternion.Euler(0, 0, m_angle / (m_wayNum - 1) * i - m_angle / 2) * v;
                v *= m_bulletSpeed;
                InstantiateAndColor(v);
            }
        }
    }

    public void Installation()
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            m_timer = 0;

            InstantiateAndColor(Vector2.zero);
        }
    }
}

[RequireComponent(typeof(Rigidbody2D))]
public class BulletClass : MonoBehaviour
{
    /// <summary> 速度</summary>
    [SerializeField] public float m_speed = 1f;
    /// <summary> 攻撃力 </summary>
    [SerializeField] public int m_power = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
