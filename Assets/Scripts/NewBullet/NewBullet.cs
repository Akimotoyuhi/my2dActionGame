using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBullet : MonoBehaviour
{
    [System.NonSerialized] private float m_endSpeed;
    [System.NonSerialized] private float m_startSpeed;
    [System.NonSerialized] private float m_curve;
    [System.NonSerialized] public int m_power;
    [SerializeField] private GameObject m_effect;
    Vector2 v;
    private Rigidbody2D m_rb;

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
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }

    public void SetParameter(float maxSpeed, float minSpeed, float curve, int power)
    {
        m_endSpeed = maxSpeed;
        m_startSpeed = minSpeed;
        m_curve = curve;
        m_power = power;
    }

    private void OnDestroy()
    {
        Instantiate(m_effect, this.transform.position, Quaternion.identity);
    }
}
