using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>敵の体力</summary>
    [SerializeField] float m_life = 10;
    /// <summary>敵の攻撃力</summary>
    [SerializeField] float m_power = 2;
    /// <summary>敵が弾を発射する間隔（秒）</summary>
    [SerializeField] float m_fireInterval = 1f;
    /// <summary>扇状に弾を出す範囲(度)</summary>
    //[SerializeField] float m_angle = 45f;
    /// <summary>way数</summary>
    //[SerializeField] int m_wayNum = 3;
    /// <summary>敵が弾を発射する場所</summary>
    [SerializeField] Transform[] m_muzzles = null;
    /// <summary>敵の弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab = null;
    /// <summary> 弾幕タイプ </summary>
    [SerializeField] PatternType patternType = PatternType.single;
    float m_timer;
    [SerializeField] BulletController.MoveDirection m_moveDirection = BulletController.MoveDirection.aimAtPlayer;
    Rigidbody2D m_rb = null;
    GameObject m_player;

    public enum PatternType
    {
        single,
        way,
    }

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.FindGameObjectWithTag("Player");

        if (m_muzzles == null)
        {
            Debug.Log("Muzzleが設定されてないよ！");
        }
    }

    
    void Update()
    {
        //プレイヤーの位置によって自身の身体の向きを変えるだけ
        if (m_player.transform.position.x < this.gameObject.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (patternType == PatternType.single)
        {
            Single();
        }
        if (patternType == PatternType.way)
        {
            way();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.name == "Player")
            {
                return;
            }
            BulletController bullet = collision.GetComponent<BulletController>();
            
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

    void way()
    {
        // angleで角度を指定、wayNumでway数を指定出来るNway弾を作りたい


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
}
