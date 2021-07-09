using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : BulletBase
{
    float x;
    float y;
    float timer = 0f;
    /// <summary>円の大きさ</summary>
    [SerializeField] float radius = 1f;
    Vector2 m_defPos;

    private void Start()
    {
        //m_defPos = transform.position;
    }

    private void Update()
    {
        x = radius * Mathf.Sin(timer * m_maxSpeed);
        y = radius * Mathf.Cos(timer * m_maxSpeed);

        timer += Time.deltaTime;
        radius += 0.01f;

        transform.position = new Vector2(x + m_defPos.x, y + m_defPos.y);
    }
}
