using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single_RandomSpeed : Muzzle
{
    [SerializeField] private float _minSpeed = 1;

    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            m_timer = 0f;

            Vector2 v = Vector2.down;
            v.Normalize();
            float speed = Random.Range(_minSpeed, m_bulletSpeed);
            v *= speed;
            var t = Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);
            t.GetComponent<Rigidbody2D>().velocity = v;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
