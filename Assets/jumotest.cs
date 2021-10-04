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
    private Rigidbody2D m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_isground = m_ground.IsGrouded();

        if (m_isground)
        {
            //ジャンプ
            m_ftimer = 0;
            if (Input.GetKey(KeyCode.Space))
            {
                m_jtimer += Time.deltaTime;
            }
        }
        else
        {
            //自由落下
            //公式;
            //v = gt
            //y = 1/2gt^2
            //v^2 = 2gy
            m_jtimer = 0;
            m_ftimer += Time.deltaTime;
            m_y = m_grv * m_ftimer;
            //float y = 1 / 2 * m_grv * m_ftimer;
            //float v2 = Mathf.Sqrt(2 * m_grv * y);
        }
        m_rb.velocity = new Vector2(0, m_y);
    }

    private void Fall()
    {
    }

    private void Jump()
    {
        if (m_isground)
        {

        }
    }
}
