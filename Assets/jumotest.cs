using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumotest : MonoBehaviour
{
    /// <summary>重力</summary>
    private float m_grv = -9.8f;
    /// <summary>y座標</summary>
    private float m_y;
    /// <summary>初速度</summary>
    [SerializeField] float m_startSpeed = 0;
    /// <summary>加速度</summary>
    [SerializeField] float m_addSpeed = 0;
    private float m_ftimer = 0;
    private float m_jtimer = 0;
    [SerializeField] private IsGrounded m_ground;
    private bool m_isground;
    private bool m_isJump = false;
    private Rigidbody2D m_rb;
    Transform m_transform;
    [SerializeField] float y = 0;
    [SerializeField] float v = 0;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
    }

    void Update()
    {
        m_isground = m_ground.IsGrouded();
        m_transform = transform;
        if (m_isground)
        {
            //ジャンプ
            m_ftimer = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_isJump = true;
                //m_y = m_startSpeed;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                m_jtimer += Time.deltaTime;
                //m_y = m_addSpeed + (m_grv * m_jtimer);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //m_isJump = false;
            }
        }
        if (m_isJump)
        {
            v = m_startSpeed - m_grv * Time.deltaTime;
            Jump();
            Fall();
        }
        //else
        //{
        //    //自由落下
        //    m_jtimer = 0;
        //    m_ftimer += Time.deltaTime;
        //    m_y = m_grv * m_ftimer;
        //}
        //m_rb.velocity = new Vector2(0, m_y);
    }

    private void Fall()
    {
        if (v < 0.1f)
        {
            y = - m_grv * Time.deltaTime * Time.deltaTime / 2f;
        }
    }

    private void Jump()
    {
        if (v > 0.1f)
        {
            y = m_startSpeed * Time.deltaTime - m_grv * Time.deltaTime * Time.deltaTime / 2f;
        } 
    }
}
