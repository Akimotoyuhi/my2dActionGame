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
    void Update()
    {
        if (directionRotation == DirectionOfRotation.Right)
        {
            if (!m_isBullet)
            {
                StartCoroutine(RightUzumaki(m_vector));
            }
        }
        if (directionRotation == DirectionOfRotation.Left) 
        {
            if (!m_isBullet)
            {
                StartCoroutine(LeftUzumaki(m_vector));
            }
        }
    }


    public IEnumerator RightUzumaki(Vector2 vec)
    {
        for (int i = 0; i < m_wayNum; i++)
        {
            Vector2 v = vec;
            v.Normalize();
            v = Quaternion.Euler(0, 0, m_angle / m_wayNum * i) * v;
            v *= m_maxSpeed;
            InstantiateAndColor(v);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator LeftUzumaki(Vector2 vec)
    {
        m_isBullet = true;
        for (int i = 0; i < m_wayNum; i++)
        {
            Vector2 v = vec;
            v.Normalize();
            v = Quaternion.Euler(0, 0, m_angle / m_wayNum * -i) * v;
            v *= m_maxSpeed;
            InstantiateAndColor(v);
            yield return new WaitForSeconds(0.1f);
        }
        m_isBullet = false;
    }
}
