using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>体力</summary>
    [SerializeField] public float m_life = 10;
    /// <summary>攻撃力</summary>
    [SerializeField] public float m_power = 2;
    /// <summary>移動するかどうか</summary>
    [SerializeField] public bool m_move = true;
    /// <summary>移動速度</summary>
    [SerializeField] public float m_speed = 1;
    /// <summary>弾を撃つかどうか</summary>
    [SerializeField] public bool m_fire = true;
    /// <summary>発射する弾の速度</summary>
    [SerializeField] public float m_bulletSpeed = 1f;
    /// <summary>弾を発射する間隔（秒）</summary>
    [SerializeField] public float m_fireInterval = 1f;
    /// <summary>扇状に弾を出す範囲(度)</summary>
    [SerializeField] public float m_angle = 45f;
    /// <summary>way数</summary>
    [SerializeField] public int m_wayNum = 3;
    /// <summary>ジャンプ力</summary>
    [SerializeField] public float m_jumpPower = 5;
    /// <summary>移動間隔</summary>
    [SerializeField] public float m_moveinterval = 3;
    /// <summary>弾を発射する場所</summary>
    [SerializeField] Transform[] m_muzzles = null;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] GameObject[] m_bulletPrefab = null;
    float m_timer;
    //public Rigidbody2D m_rb = null;

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

    public void AtPlayer()
    {
        GameObject player = GameObject.Find("Player");
        if (player)
        {
            //プレイヤーの位置によって自身の身体の向きを変えるだけ
            if (player.transform.position.x < this.gameObject.transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }
    }

    /// <summary> 単発の弾を連発する </summary>
    public void Single()
    {
        if (m_bulletPrefab[0])
        {
            // 一定間隔で弾を発射する
            m_timer += Time.deltaTime;
            if (m_timer > m_fireInterval)
            {
                m_timer = 0f;

                // 各 muzzle から弾を発射する
                foreach (Transform t in m_muzzles)
                {
                    Instantiate(m_bulletPrefab[0], t.position, Quaternion.identity);
                }
            }
        }
    }

    /// <summary>
    /// 角度とway数を元にNway弾を撃つ
    /// </summary>
    /// <param name="muzzleNum">この弾を撃ちたいmuzzleの配列番号</param>
    /// <param name="prefabNum">どのプレハブの弾を撃つか</param>
    public void Way(int muzzleNum, int prefabNum)
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
                    var t = Instantiate(m_bulletPrefab[prefabNum], m_muzzles[muzzleNum].position, Quaternion.identity);
                    t.GetComponent<Rigidbody2D>().velocity = v;
                }
            }
        }
    }
}
