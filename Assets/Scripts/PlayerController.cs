﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    /// <summary> 移動速度 </summary>
    [SerializeField] float m_moveSpeed = 5f;
    /// <summary> ジャンプ速度 </summary>
    [SerializeField] float m_jumpPower = 10f;
    /// <summary> 弾を発射する間隔（秒）</summary>
    [SerializeField] float m_fireInterval = 0.5f;
    /// <summary> 最大体力 </summary>
    [SerializeField] int m_maxLife = 10;
    /// <summary> 体力 </summary>
    [SerializeField] int m_life = 10;
    /// <summary> 弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab = null;
    [SerializeField] float m_bulletSpeed = 15;
    Rigidbody2D m_rb = null;
    Animator m_anim = null;
    Slider m_slider = null;
    GameObject m_canvas = null;
    SpriteRenderer m_spriteRenderer = null;
    [SerializeField] IsGrounded ground = null;
    [SerializeField] bool godMode = false;
    bool m_isGround = false;
    bool m_isJump = false;
    bool m_damage = false;
    float m_timer = 0;
    float m_bulletTimer = 1;
    bool m_isrelease = false;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_canvas = GameObject.Find("Canvas");
        m_slider = m_canvas.transform.Find("PlayerHPgage").GetComponent<Slider>();
        m_slider.maxValue = m_maxLife;
    }
    void Update()
    {
        m_slider.value = m_life;
        float horizontalKey = Input.GetAxisRaw("Horizontal");
        float xSpeed = 0f;
        m_isGround = ground.IsGrouded();

        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            m_anim.SetBool("Run", true);
            xSpeed = m_moveSpeed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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
                BulletClass bullet = collision.GetComponent<BulletClass>();
                m_life -= bullet.m_power;
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
                StartCoroutine("DamageTimer");
            }
        }
        
        if (m_life <= 0)
        {
            Destroy(gameObject);
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
            m_timer = 0;
            m_isrelease = false;
        }
        if (Input.GetButtonUp("Jump"))
        {
            if (m_isrelease) return; ;
            isrel = true;
            m_isrelease = true;
        }
        if (Input.GetButton("Jump")) 
        { 
            m_timer += Time.deltaTime; 
        }

        if (m_isGround)
        {
            // 入力の最大値を超えたら強制的に飛ぶ
            if (m_timer > 0.2 && !m_isrelease)
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
    
    private void Fire()
    {
        m_bulletTimer += Time.deltaTime;
        if (m_bulletTimer > m_fireInterval)
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
                v *= m_bulletSpeed;
                var t = Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);
                t.GetComponent<Rigidbody2D>().velocity = v;
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
