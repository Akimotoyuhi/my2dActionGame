using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nway : Muzzle
{
    /// <summary>扇状に弾を出す範囲(度)</summary>
    [SerializeField] public float m_angle = 45f;
    /// <summary>way数</summary>
    [SerializeField] public int m_wayNum = 3;

    void Update()
    {
        if (_pattenName == Pattern.Aim_at_Player)
        {
            StartCoroutine(Way());
        }
        if (_pattenName == Pattern.Designation)
        {
            StartCoroutine(Way(m_vector));
        }
    }

    /// <summary>
    /// Angleで角度、WayNumでway数を指定して自機狙いのNway弾を撃つ
    /// </summary>
    public IEnumerator Way()
    {
        GameObject player = GameObject.Find("Player");
        if (player && !m_isBullet)
        {
            m_isBullet = true;
            yield return new WaitForSeconds(m_fireInterval);

            for (int i = 0; i < m_barrage; i++)
            {
                for (int n = 0; n < m_wayNum; n++)
                {
                    Vector2 v = player.transform.position - this.transform.position;
                    v.Normalize();
                    //狙った座標が中心になるように発射角をズラす
                    v = Quaternion.Euler(0, 0, m_angle / (m_wayNum - 1) * n - m_angle / 2) * v;
                    v *= m_maxSpeed;
                    InstantiateAndColor(v);
                }
                yield return new WaitForSeconds(m_barrageTime);
            }
            m_isBullet = false;
        }
    }

    /// <summary>
    /// Angleで角度、WayNumでway数を指定してNway弾を撃つ
    /// </summary>
    /// <param name="v">射出方向</param>
    public IEnumerator Way(Vector2 vec)
    {
        if (!m_isBullet)
        {
            m_isBullet = true;
            yield return new WaitForSeconds(m_fireInterval);

            for (int i = 0; i < m_barrage; i++)
            {
                for (int n = 0; n < m_wayNum; n++)
                {
                    Vector2 v = vec;
                    v.Normalize();
                    v = Quaternion.Euler(0, 0, m_angle / (m_wayNum - 1) * n - m_angle / 2) * v;
                    v *= m_maxSpeed;
                    InstantiateAndColor(v);
                }
                yield return new WaitForSeconds(m_barrageTime);
            }
            m_isBullet = false;
        }
    }
}
