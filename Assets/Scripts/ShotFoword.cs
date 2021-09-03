using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFoword : MonoBehaviour
{
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Color m_color;
    //[SerializeField] private float m_maxSpeed;
    [SerializeField] private float m_minSpeed;
    [SerializeField] private int m_bulletPower;
    [SerializeField] private float m_fireInterval;
    private float timer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > m_fireInterval)
        {
            timer = 0;
            TestInstantiete();
        }
    }

    private void TestInstantiete()
    {
        var t = Instantiate(m_bulletPrefab, this.transform.position, this.transform.rotation);
        if (t.GetComponent<SpriteRenderer>())
        {
            t.GetComponent<SpriteRenderer>().color = m_color;
        }
        TestBullet m_bullet = t.GetComponent<TestBullet>();
        if (m_bullet)
        {
            //m_bullet.m_maxSpeed = m_maxSpeed;
            m_bullet.m_minSpeed = m_minSpeed;
            m_bullet.m_power = m_bulletPower;
        }
    }
}
