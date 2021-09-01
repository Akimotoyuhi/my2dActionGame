using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baramaki : Nway
{

    public override void OnShot()
    {
        base.OnShot();
    }

    public override IEnumerator Shot()
    {
        if (m_player && !m_isBullet)
        {
            m_isBullet = true;

            yield return new WaitForSeconds(m_direyTime);

            for (int i = 0; i < m_barrage; i++)
            {
                for (int n = 0; n < m_wayNum; n++)
                {
                    Vector2 v = m_player.transform.position - this.transform.position;
                    v.Normalize();
                    v = Quaternion.Euler(0, 0, m_angle / (m_wayNum - 1) * n - m_angle / 2) * v;
                    v *= m_maxSpeed;
                    InstantiateAndColor(v);
                }
                yield return new WaitForSeconds(m_barrageTime);
            }
            yield return new WaitForSeconds(m_fireInterval);
            m_isBullet = false;
        }
    }

    public override IEnumerator Shot(Vector2 vec)
    {
        if (!m_isBullet)
        {
            m_isBullet = true;

            yield return new WaitForSeconds(m_direyTime);

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
            yield return new WaitForSeconds(m_fireInterval);

            m_isBullet = false;
        }
    }
}
