﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public enum Wepon
{
    Normal = 0,
    Blast = 1
}

public enum StatusItems
{
    Life = 0,
    Mana = 1,
    Power = 2
}

public class PlayerController : MonoBehaviour
{
    /// <summary> 攻撃力 </summary>
    [SerializeField] private int m_power = 2;
    /// <summary> 移動速度 </summary>
    [SerializeField] private float m_moveSpeed = 5f;
    /// <summary> ジャンプ速度 </summary>
    [SerializeField] private float m_jumpPower = 10f;
    /// <summary> 弾を発射する間隔（秒）</summary>
    [SerializeField] private float m_fireInterval = 0.5f;
    /// <summary> 最大体力 </summary>
    [SerializeField] private int m_maxLife = 10;
    /// <summary> 現在体力 </summary>
    [SerializeField] private int m_life = 10;
    /// <summary> 最大マナ </summary>
    [SerializeField] private int m_maxMana = 50;
    /// <summary> 現在マナ </summary>
    [SerializeField] private int m_mana = 50;
    /// <summary> マナが回復するまでの時間 </summary>
    [SerializeField] private float m_manaRegeneTime = 0.5f;
    /// <summary> 弾のプレハブ</summary>
    [SerializeField] private GameObject[] m_bulletPrefab;
    /// <summary> 弾の速度</summary>
    [SerializeField] private float m_bulletSpeed = 15;
    /// <summary> 現在所持している武器の数</summary>
    public int m_haveBullet = 0;
    /// <summary> 選択中の攻撃(配列要素)</summary>
    private int m_selectBulletIndex = 0;
    /// <summary> ステータスアップアイテム用</summary>
    [System.NonSerialized] public int[] m_haveItem = { 0, 0, 0 };
    private GameObject m_playerUi = null;
    private GameObject[] m_bulletSprites;
    private Rigidbody2D m_rb = null;
    private Animator m_anim = null;
    private Slider m_hpSlider = null;
    private Slider m_mpSlider = null;
    private GameObject m_canvas = null;
    private CinemachineConfiner m_vcam = null;
    private SpriteRenderer m_spriteRenderer = null;
    private GameManager m_gamemanager = null;
    [SerializeField] private IsGrounded ground = null;
    [SerializeField] private bool godMode = false;
    private bool m_isGround = false;
    //private bool m_isJump = false;
    private bool m_damage = false;
    private float m_timer = 0;
    private float m_bulletTimer = 1;
    private float m_mpTimer = 0;
    private bool m_isrelease = false;

    enum AttackMana
    {
        Normal = 2,
        Blast = 10
    }

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_canvas = GameObject.Find("Canvas");
        m_playerUi = m_canvas.transform.Find("PlayerStateUI").gameObject;
        m_hpSlider = m_playerUi.transform.Find("HPgage").GetComponent<Slider>();
        m_hpSlider.maxValue = m_maxLife;
        m_mpSlider = m_playerUi.transform.Find("MPgage").GetComponent<Slider>();
        m_mpSlider.maxValue = m_maxMana;
        GameObject BulletType = m_playerUi.transform.Find("BulletType").gameObject;
        m_bulletSprites = new GameObject[BulletType.transform.childCount];
        for (int i = 0; i < BulletType.transform.childCount; i++)
        {
            m_bulletSprites[i] = BulletType.transform.GetChild(i).gameObject;
        }
        m_gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        m_hpSlider.value = m_life;
        m_mpSlider.value = m_mana;
        BulletSpriteActiveChanged();
    }
    void Update()
    {
        m_hpSlider.value = m_life;
        float horizontalKey = Input.GetAxisRaw("Horizontal");
        float xSpeed = 0f;
        m_isGround = ground.IsGrouded();

        if (horizontalKey > 0)
        {
            transform.localScale = new Vector2(1, 1);
            m_anim.SetBool("Run", true);
            xSpeed = m_moveSpeed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            m_anim.SetBool("Run", true);
            xSpeed = -m_moveSpeed;
        }
        else
        {
            m_anim.SetBool("Run", false);
            xSpeed = 0f;
        }

        Jump();

        Fire();

        AttackChanged();

        ManaRegene();

        // 落下速度を上げたい
        //if (m_isJump)
        //{
        //    m_rb.velocity = new Vector2(xSpeed, m_rb.velocity.y);
        //}
        //else
        //{
        //    m_rb.velocity = new Vector2(xSpeed, m_rb.velocity.y * 1.2f);
        //}
        m_rb.velocity = new Vector2(xSpeed, m_rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!godMode)
        {
            if (collision.tag == "EnemyBullet")
            {
                if (m_damage)
                {
                    return;
                }
                BulletBase bullet = collision.GetComponent<BulletBase>();
                m_life -= bullet.m_power;
                m_hpSlider.value = m_life;
                StartCoroutine("DamageTimer");
            }
            if (collision.tag == "Enemy")
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (m_damage)
                {
                    return;
                }
                m_life -= enemy.m_power;
                m_hpSlider.value = m_life;
                StartCoroutine("DamageTimer");
            }
        }
        
        //死亡時の処理
        if (m_life <= 0)
        {
            m_gamemanager.PlayerDead();
            Destroy(gameObject);
        }

        //Cinemachineのカメラ制御
        if (collision.tag == "CameraCollider")
        {
            if (m_vcam)
            {
                m_vcam.m_BoundingShape2D = collision;
            }
            else
            {
                m_vcam = gameObject.transform.Find("CM vcam").GetComponent<CinemachineConfiner>();
                m_vcam.m_BoundingShape2D = collision;
            }
            //コライダーに入ったらGameManagerに入った事を伝えて、敵を出現させる。
            m_gamemanager.EnemySpawning();
        }

        //チェックポイント変更
        if (collision.tag == "Checkpoint")
        {
            m_gamemanager.m_spawnPoint = collision.transform.position;
        }
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump()
    {
        bool isrel = false;
        if (Input.GetButtonDown("Jump"))
        {
            // 押されたと同時にタイマーとフラグをリセット
            m_timer = 0;
            m_isrelease = false;
        }
        if (Input.GetButtonUp("Jump"))
        {
            // 途中でジャンプボタンから手を離した
            if (m_isrelease) return; ;
            isrel = true;
            m_isrelease = true;
        }
        if (Input.GetButton("Jump")) 
        { 
            // ジャンプボタンが押されている時間を数える
            m_timer += Time.deltaTime; 
        }

        if (m_isGround)
        {
            // 入力の最大値を超えたら強制的に飛ぶ
            if (m_timer > 0.1 && !m_isrelease)
            {
                m_rb.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);
                m_anim.SetTrigger("Jump");
                m_isrelease = true;
                return;
            }
            if (isrel)
            {
                m_rb.AddForce(Vector2.up * (m_jumpPower / 1.5f), ForceMode2D.Impulse);
                m_anim.SetTrigger("Jump");
            }
        }
    }
    
    /// <summary>
    /// 攻撃
    /// </summary>
    private void Fire()
    {
        m_bulletTimer += Time.deltaTime;
        if (m_bulletTimer > m_fireInterval && m_mana > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                m_bulletTimer = 0;
                Vector2 v;
                if (this.transform.localScale.x > 0)
                {
                    v = Vector2.right;
                }
                else
                {
                    v = Vector2.left;
                }
                v.Normalize();
                var t = Instantiate(m_bulletPrefab[m_selectBulletIndex], this.transform.position, Quaternion.identity);
                ManaPointSub();
                BulletBase bullet = t.GetComponent<BulletBase>();
                if (bullet)
                {
                    bullet.m_minSpeed = m_bulletSpeed;
                    bullet.m_power = m_power;
                    bullet.m_velo = v;
                }
            }
        }
    }

    /// <summary>
    /// 攻撃によってMPが減る処理
    /// </summary>
    private void ManaPointSub()
    {
        if (m_selectBulletIndex == (int)Wepon.Normal)
        {
            m_mana -= (int)AttackMana.Normal;
        }
        if (m_selectBulletIndex == (int)Wepon.Blast)
        {
            m_mana -= (int)AttackMana.Blast;
        }
    }

    /// <summary>
    /// 時間経過でマナが自然回復する処理
    /// </summary>
    private void ManaRegene()
    {
        m_mpTimer += Time.deltaTime;
        if (m_mpTimer > m_manaRegeneTime && m_mana < m_maxMana)
        {
            m_mpTimer = 0;
            m_mana++;
            m_mpSlider.value = m_mana;
        }
    }

    /// <summary>
    /// 攻撃の切り替え
    /// </summary>
    private void AttackChanged()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            m_selectBulletIndex++;
        }

        if (m_selectBulletIndex > m_haveBullet)
        {
            m_selectBulletIndex = 0;
        }
        if (m_selectBulletIndex < 0)
        {
            m_selectBulletIndex = m_haveBullet;
        }

        BulletSpriteActiveChanged();
    }

    private void BulletSpriteActiveChanged()
    {
        for (int i = 0; i < m_bulletSprites.Length; i++)
        {
            if (m_selectBulletIndex == i)
            {
                m_bulletSprites[i].SetActive(true);
            }
            else
            {
                m_bulletSprites[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// ダメージを受けた時の無敵時間
    /// </summary>
    /// <returns></returns>
    IEnumerator DamageTimer()
    {
        if (m_damage)
        {
            yield break;
        }

        m_damage = true;

        // 無敵時間中の点滅
        for (int i = 0; i < 10; i++)
        {
            m_spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.05f);
            m_spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }

        m_damage = false;
    }
}
