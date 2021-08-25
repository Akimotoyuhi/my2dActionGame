using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    /// <summary>最大体力</summary>
    [SerializeField] public int m_maxLife = 10;
    /// <summary>体力</summary>
    [SerializeField] public int m_life = 10;
    /// <summary>攻撃力</summary>
    [SerializeField] public int m_power = 2;
    /// <summary>移動速度</summary>
    [SerializeField] public float m_speed = 1;
    /// <summary>ジャンプ力</summary>
    [SerializeField] public float m_jumpPower = 5;
    /// <summary>移動間隔</summary>
    [SerializeField] public float m_moveinterval = 3;
    /// <summary>移動するか</summary>
    [SerializeField] public bool m_move = true;
    /// <summary>移動間隔用タイマー</summary>
    [System.NonSerialized] public float m_moveTimer;
    /// <summary>現在移動中か</summary>
    [System.NonSerialized] public bool m_isMove = false;
    /// <summary>ダメージ表示用キャンバス</summary>
    [SerializeField] private GameObject m_damagePrefab;
    [System.NonSerialized] public GameObject m_player = null;
    [System.NonSerialized] public Rigidbody2D m_rb = null;


    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.Find("Player");
    }

    /// <summary>
    /// プレイヤーの位置によって自身の身体の向きを変える
    /// </summary>
    public void AtPlayer()
    {
        GameObject player = GameObject.Find("Player");
        if (player)
        {
            if (player.transform.position.x < this.gameObject.transform.position.x)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet" || collision.tag == "Blast")
        {
            BulletBase bullet = collision.GetComponent<BulletBase>();
            m_life -= bullet.m_power;
            Vector2 v = new Vector2(this.transform.position.x + Random.Range(-1f, 1f), this.transform.position.y + Random.Range(-1f, 1f));
            var inst = Instantiate(m_damagePrefab, v, Quaternion.identity);
            inst.GetComponent<DamageText>().m_vec = v;
            Text text = inst.transform.GetChild(0).GetComponent<Text>();
            text.text = $"{bullet.m_power}";
        }

        if (m_life <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public abstract void Move();
}
