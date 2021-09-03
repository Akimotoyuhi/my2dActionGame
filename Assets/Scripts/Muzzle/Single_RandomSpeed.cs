using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single_RandomSpeed : Muzzle
{
    void Update()
    {
        if (_pattenName == Pattern.Aim_at_Player)
        {
            RandomSpeed();
        }
        if (_pattenName == Pattern.Designation)
        {
            RandomSpeed(m_vector);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 自機狙いの単発の弾をランダムな速度で撃つ
    /// </summary>
    private void RandomSpeed()
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            m_timer = 0f;

            Vector2 v = Vector2.down;
            v.Normalize();
            float speed = Random.Range(m_minSpeed, m_maxSpeed);
            v *= speed;
            InstantiateAndColor(v);
        }
    }

    public void RandomSpeed(Vector2 vec)
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            m_timer = 0f;

            Vector2 v = vec;
            v.Normalize();
            float speed = Random.Range(m_minSpeed, m_maxSpeed);
            v *= speed;
            InstantiateAndColor(v);
        }
    }

    public override void OnShot()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Shot()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Shot(Vector2 vec)
    {
        throw new System.NotImplementedException();
    }
}
