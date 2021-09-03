using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzumaki : Nway
{
    [SerializeField] private DirectionOfRotation directionRotation = DirectionOfRotation.Left;
    private enum DirectionOfRotation
    {
        Right,
        Left
    }

    public override void OnShot()
    {
        if (directionRotation == DirectionOfRotation.Right)
        {
            StartCoroutine(RightUzumaki(m_vector));
        }
        if (directionRotation == DirectionOfRotation.Left)
        {
            StartCoroutine(LeftUzumaki(m_vector));
        }
    }


    public IEnumerator RightUzumaki(Vector2 vec)
    {
        if (!m_isBullet)
        {
            m_isBullet = true;
            yield return new WaitForSeconds(m_direyTime);
            for (int i = 0; i < m_shotnum; i++)
            {
                for (int n = 0; n < m_wayNum; n++)
                {
                    Vector2 v = vec;
                    v.Normalize();
                    v = Quaternion.Euler(0, 0, m_angle / m_wayNum * n) * v;
                    v *= m_maxSpeed;
                    InstantiateAndColor(v);
                    yield return new WaitForSeconds(m_fireInterval);
                }
            }
            m_isBullet = false;
        }
    }

    public IEnumerator LeftUzumaki(Vector2 vec)
    {
        if (!m_isBullet)
        {
            m_isBullet = true;
            yield return new WaitForSeconds(m_direyTime);
            for (int i = 0; i < m_shotnum; i++)
            {
                for (int n = 0; n < m_wayNum; n++)
                {
                    Vector2 v = vec;
                    v.Normalize();
                    v = Quaternion.Euler(0, 0, m_angle / m_wayNum * -n) * v;
                    v *= m_maxSpeed;
                    InstantiateAndColor(v);
                    yield return new WaitForSeconds(m_fireInterval);
                }
            }
            m_isBullet = false;
        }
    }
}
