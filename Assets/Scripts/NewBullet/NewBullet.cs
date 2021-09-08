using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBullet : MonoBehaviour
{
    //[SerializeField] private GameObject m_effectPrefab;
    [System.NonSerialized] private float m_maxSpeed;
    [System.NonSerialized] private float m_minSpeed;
    [System.NonSerialized] private float m_curve;
    [System.NonSerialized] public int m_power;
    Vector2 v;
    private Rigidbody2D m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        v = transform.rotation * Vector2.up;
        v.Normalize();
        m_rb.velocity = v * m_minSpeed;
    }

    void Update()
    {
        transform.Rotate(0, 0, m_curve);
        v = transform.rotation * Vector2.up;
        m_rb.velocity = v * m_minSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }

    public void SetParameter(float maxSpeed, float minSpeed, float curve, int power)
    {
        m_maxSpeed = maxSpeed;
        m_minSpeed = minSpeed;
        m_curve = curve;
        m_power = power;
    }
}
