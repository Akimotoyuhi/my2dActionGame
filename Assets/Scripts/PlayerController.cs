using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary> 移動速度 </summary>
    [SerializeField] float m_moveSpeed = 5f;
    /// <summary> ジャンプ速度 </summary>
    [SerializeField] float m_jumpPower = 10f;
    /// <summary>敵が弾を発射する間隔（秒）</summary>
    [SerializeField] float m_fireInterval = 0.5f;
    /// <summary> 自機の体力 </summary>
    [SerializeField] int m_life = 10;
    /// <summary>自機が弾を発射する場所</summary>
    [SerializeField] Transform[] m_muzzles = null;
    /// <summary>自機の弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab = null;
    Rigidbody2D m_rb = null;
    Animator m_anim = null;
    SpriteRenderer m_spriteRenderer = null;
    [SerializeField] IsGrounded ground = null;
    bool m_isGround = false;
    bool m_damage = false;
    //bool m_isJump = false;
    //float m_jumpTimer = 0f;
    [SerializeField] float m_jumpTimerLimit = 2f;
    float m_timer;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float horizontalKey = Input.GetAxisRaw("Horizontal");
        float xSpeed = 0.0f;
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
            xSpeed = 0.0f;
        }

        /*
        //Jumpキーを押した時間によってジャンプの高さをを変えたい
        if (Input.GetButton("Jump") && m_isGround)
        {
            if (m_jumpTimer < m_jumpTimerLimit)
            {
                m_jumpTimer = Time.deltaTime;
            }

            if (m_jumpTimer > 0)
            {
                if (m_jumpTimer < 1f)
                {
                    m_rb.AddForce(Vector2.up * (m_jumpPower / 2), ForceMode2D.Impulse);
                    m_anim.SetTrigger("Jump");
                    m_jumpTimer = 0;
                }
                else if (m_jumpTimer >= 1f)
                {
                    m_rb.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);
                    m_anim.SetTrigger("Jump");
                    m_jumpTimer = 0;
                }
            }
        }
        */

        if (Input.GetButtonDown("Jump") && m_isGround)
        {
            m_rb.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);
            m_anim.SetTrigger("Jump");
        }
        m_timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
        {
            if(m_timer > m_fireInterval)
            {
                m_timer = 0f;
                foreach (Transform t in m_muzzles)
                {
                    Instantiate(m_bulletPrefab, t.position, Quaternion.identity);
                }
            }
        }

        m_rb.velocity = new Vector2(xSpeed, m_rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            BulletController bullet = collision.GetComponent<BulletController>();
            if (m_damage)
            {
                return;
            }

            m_life = m_life - bullet.m_power;

            if (m_life <= 0)
            {
                Destroy(gameObject);
            }
            StartCoroutine("DamageTimer");
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
