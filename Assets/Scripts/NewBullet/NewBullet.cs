using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBullet : MonoBehaviour
{
    private float m_endSpeed;
    private float m_startSpeed;
    private float m_speedUp;
    private float m_curve;
    [System.NonSerialized] public int m_power;
    [SerializeField] private GameObject m_effect;
    private Vector2 v;
    private Rigidbody2D m_rb;
    private bool m_isquitting = false;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        v = transform.rotation * Vector2.up;
        v.Normalize();
        m_rb.velocity = v * m_startSpeed;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Rotate(0, 0, m_curve);
        v = transform.rotation * Vector2.up;
        m_rb.velocity = v * m_startSpeed;
        if (m_speedUp > 0)
        {
            if (m_startSpeed <= m_endSpeed)
            {
                m_startSpeed += m_speedUp;
            }
        }
        else if (m_speedUp < 0)
        {
            if (m_startSpeed >= m_endSpeed)
            {
                m_startSpeed += m_speedUp;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }

    public void SetParameter(float endSpeed, float startSpeed, float speedUp, float curve, int power)
    {
        m_endSpeed = endSpeed;
        m_startSpeed = startSpeed;
        m_speedUp = speedUp;
        m_curve = curve;
        m_power = power;
    }

    private void OnApplicationQuit()
    {
        m_isquitting = true;
    }

    private void OnDestroy()
    {
        //シーン実行終了時にゴミが残ってしまうエラー回避用
        if (!m_isquitting)
        {
            Instantiate(m_effect, this.transform.position, Quaternion.identity);
        }
    }
}
