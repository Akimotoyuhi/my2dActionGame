using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBase : MonoBehaviour
{
    /// <summary>最大速度</summary>
    [System.NonSerialized] public float m_maxSpeed;
    /// <summary>最低速度</summary>
    [System.NonSerialized] public float m_minSpeed;
    /// <summary>攻撃力</summary>
    [System.NonSerialized] public int m_power;
    /// <summary>進行方向</summary>
    [System.NonSerialized] public Vector2 m_velo;
    [System.NonSerialized] public Rigidbody2D m_rb;

    public void InitialVelocity()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_velo *= m_minSpeed;
        m_rb.velocity = m_velo;
    }
}