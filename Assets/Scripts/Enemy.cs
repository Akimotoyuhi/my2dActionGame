using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>体力</summary>
    [SerializeField] public float m_life = 10;
    /// <summary>攻撃力</summary>
    [SerializeField] public float m_power = 2;
    /// <summary>移動速度</summary>
    [SerializeField] public float m_speed = 1;
    /// <summary>弾を発射する間隔（秒）</summary>
    [SerializeField] public float m_fireInterval = 1f;
    /// <summary>ジャンプ力</summary>
    [SerializeField] public float m_jumpPower = 5;
    /// <summary>移動間隔</summary>
    [SerializeField] public float m_moveinterval = 3;
    /// <summary>扇状に弾を出す範囲(度)</summary>
    //[SerializeField] float m_angle = 45f;
    /// <summary>way数</summary>
    //[SerializeField] int m_wayNum = 3;
    /// <summary>弾を発射する場所</summary>
    [SerializeField] Transform[] m_muzzles = null;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab = null;
    //public GameObject m_player;
    public float m_timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            BulletClass bullet = collision.GetComponent<BulletClass>();
            m_life = m_life - bullet.m_power;

            if (m_life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    /// <summary> 単発の弾を連発する </summary>
    void Single()
    {
        if (m_bulletPrefab)
        {
            // 一定間隔で弾を発射する
            m_timer += Time.deltaTime;
            if (m_timer > m_fireInterval)
            {
                m_timer = 0f;

                // 各 muzzle から弾を発射する
                foreach (Transform t in m_muzzles)
                {
                    Instantiate(m_bulletPrefab, t.position, Quaternion.identity);
                }
            }
        }
    }

    public void AtPlayer(GameObject Player)
    {
        //プレイヤーの位置によって自身の身体の向きを変えるだけ
        if (Player.transform.position.x < this.gameObject.transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
    /*
    void way()
    {
        //angleで角度を指定、wayNumでway数を指定出来るNway弾を作りたい


        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            // 一定間隔で弾を発射する
            m_timer += Time.deltaTime;
            if (m_timer > m_fireInterval)
            {
                m_timer = 0f;

                // 各 muzzle から弾を発射する
                foreach (Transform t in m_muzzles)
                {
                    Instantiate(m_bulletPrefab, t.position, Quaternion.identity);
                }
            }
        }
    }
    */
}
