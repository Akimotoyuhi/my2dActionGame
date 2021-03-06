using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour, IDamage
{
    /// <summary>最大体力</summary>
    [SerializeField] public int m_maxLife = 1;
    /// <summary>体力</summary>
    [SerializeField] public int m_life = 1;
    /// <summary>攻撃力</summary>
    [SerializeField] public int m_power = 1;
    /// <summary>移動速度</summary>
    [SerializeField] public float m_speed = 1;
    /// <summary>ジャンプ力</summary>
    [SerializeField] public float m_jumpPower = 1;
    /// <summary>移動間隔</summary>
    [SerializeField] public float m_moveinterval = 1;
    /// <summary>発射間隔</summary>
    [SerializeField] public float m_shotinterval = 1;
    /// <summary>移動間隔用タイマー</summary>
    [System.NonSerialized] public float m_moveTimer;
    /// <summary>発射間隔用タイマー</summary>
    [System.NonSerialized] public float m_shotTimer;
    /// <summary>現在移動中か</summary>
    [System.NonSerialized] public bool m_isMove = false;
    /// <summary>ダメージ表示用キャンバス</summary>
    [SerializeField] public GameObject m_damagePrefab;
    /// <summary>弾撃つとこ</summary>
    [SerializeField] public GameObject[] m_muzzle;
    [System.NonSerialized] public GameObject m_player;
    [System.NonSerialized] public Rigidbody2D m_rb;
    [System.NonSerialized] public Animator m_anim;

    /// <summary>
    /// 敵作る時にとりあえずStartでやっておいてほしい事
    /// </summary>
    public void SetUp()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.FindWithTag("Player");
        for (int i = 0; i < m_muzzle.Length; i++)
        {
            m_muzzle[i].SetActive(false);
        }
    }

    /// <summary>
    /// 拡張版セットアップ
    /// </summary>
    public void AnimSetUp()
    {
        SetUp();
        m_anim = GetComponent<Animator>();
    }

    /// <summary>
    /// プレイヤーの位置によって自身の身体の向きを変える
    /// </summary>
    public void AtPlayer()
    {
        if (!m_player) { m_player = GameObject.FindWithTag("Player"); }
        if (m_player)
        {
            if (m_player.transform.position.x < this.gameObject.transform.position.x)
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
            OnDead();
        }
    }

    /// <summary>
    /// 死んだ時のリアクション
    /// </summary>
    private void OnDead()
    {
        if (m_rb.bodyType != RigidbodyType2D.Dynamic) { m_rb.bodyType = RigidbodyType2D.Dynamic; }
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        if (col) { col.enabled = false; }
        else { Debug.LogError("CircleCollider2D is null (Enemy.OnDead)"); }
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        Vector2 v = new Vector2(Random.Range(-1f, 1f), 1);
        m_rb.AddForce(v * 3, ForceMode2D.Impulse);
        Invoke("Dead", 1f);
    }

    /// <summary>
    /// Invoke用 呼ばれると死ぬ
    /// </summary>
    private void Dead()
    {
        Destroy(this.gameObject);
    }

    public virtual void Move()
    {
        if (m_isMove)
        {
            return;
        }
    }

    public int Damage()
    {
        return m_power;
    }
}
