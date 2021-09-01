using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single : Muzzle
{
    public override void OnShot()
    {
        if (_pattenName == Pattern.Aim_at_Player)
        {
            StartCoroutine(Shot());
        }
        if (_pattenName == Pattern.Designation)
        {
            StartCoroutine(Shot(m_vector));
        }
    }

    /// <summary> 単発の自機狙い弾を撃つ </summary>
    public override IEnumerator Shot()
    {
        if (m_player)
        {
            if (!m_isBullet)
            {
                m_isBullet = true;
                yield return new WaitForSeconds(m_fireInterval);
                for (int i = 0; i < m_barrage; i++)
                {
                    Vector2 v = m_player.transform.position - m_muzzle.position;
                    v.Normalize();
                    //v *= m_maxSpeed;
                    InstantiateAndColor(v);
                    yield return new WaitForSeconds(m_barrageTime);
                }
                m_isBullet = false;
            }
        }
    }

    /// <summary>
    /// 単発の弾を撃つ
    /// </summary>
    /// <param name="v">射出方向</param>
    public override IEnumerator Shot(Vector2 vec)
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            m_timer = 0f;

            for (int i = 0; i < m_barrage; i++)
            {
                //プレイヤーがいる方向によって撃つ方向を変える
                Vector2 v;
                if (!m_changeDirection)
                {
                    v = vec;
                }
                else
                {
                    v = SetDirection();
                }
                v.Normalize();
                v *= m_maxSpeed;
                InstantiateAndColor(v);
                yield return new WaitForSeconds(m_barrageTime);
            }
        }
    }
}
