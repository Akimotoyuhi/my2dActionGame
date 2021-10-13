using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBullet : MonoBehaviour, IDamage, IPoolable
{
    private float m_endSpeed;
    private float m_startSpeed;
    private float m_speedUp;
    private float m_curve;
    protected float m_timer;
    private float m_lifeTime;
    private int m_power;
    private Vector2 m_defPos;
    //private Quaternion m_angle;
    [SerializeField] private GameObject m_effect;
    private Vector2 v;
    private Rigidbody2D m_rb;
    private bool m_isQuiting = false;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        //this.transform.position = m_defPos;
        v = transform.rotation * Vector2.up;
        v.Normalize();
        m_rb.velocity = v * m_startSpeed;
    }

    void Update()
    {
        if (!m_renderer.enabled) { return; }
        Move();
        m_timer += Time.deltaTime;
        if (m_timer > m_lifeTime)
        {
            Destroy();
        }
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
        Destroy();
    }

    public void SetParameter(float endSpeed, float startSpeed, float speedUp, float curve, int power, float lifeTime, Color color, Quaternion angle)
    {
        m_endSpeed = endSpeed;
        m_startSpeed = startSpeed;
        m_speedUp = speedUp;
        m_curve = curve;
        m_power = power;
        m_lifeTime = lifeTime;
        if (GetComponent<SpriteRenderer>()) GetComponent<SpriteRenderer>().color = color;
        this.transform.rotation = angle;
    }

    private void OnApplicationQuit()
    {
        m_isQuiting = true;
    }

    private void OnDestroy()
    {
        //シーン実行終了時にゴミが残ってしまうエラー回避用
        if (!m_isQuiting)
        {
            Instantiate(m_effect, this.transform.position, Quaternion.identity);
        }
    }

    public void SetDefpos(Vector2 pos)
    {
        this.transform.position = pos;
    }

    //IDamageに対応
    public int Damage()
    {
        return m_power;
    }

    //オブジェクトプールに対応
    public bool IsActive => m_renderer.enabled;
    private Renderer m_renderer;

    public void DisactiveForInstantiate()
    {
        m_renderer = GetComponent<Renderer>();
        m_renderer.enabled = false;
    }

    public void Create()
    {
        m_renderer.enabled = true;
        m_timer = 0f;
        this.transform.position = m_defPos;
    }

    public void Destroy()
    {
        m_renderer.enabled = false;
    }
}