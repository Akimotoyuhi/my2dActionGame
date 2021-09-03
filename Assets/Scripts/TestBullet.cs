using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    [System.NonSerialized] public float m_minSpeed;
    [System.NonSerialized] public int m_power;
    private Rigidbody2D m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 v = transform.localPosition;
        v.Normalize();
        m_rb.velocity = v * m_minSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
