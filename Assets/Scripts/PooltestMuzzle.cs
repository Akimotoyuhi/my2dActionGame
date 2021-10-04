using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooltestMuzzle : MonoBehaviour
{
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] float m_fireInterval;
    [SerializeField] float m_speed;
    private float m_timer = 0;
    private Objectpool<PooltestBullet> m_pool = new Objectpool<PooltestBullet>();

    void Start()
    {
        
    }

    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            m_timer = 0;
            Vector2 v = new Vector2(1, 0);
            v.Normalize();
            v *= m_speed;
            GameObject obj = Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = v;
        }
    }
}
