using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : BulletBase
{
    float x;
    float y;
    Vector2 defPos;
    float timer = 0;
    /// <summary>円の大きさ</summary>
    [SerializeField] float radius = 1f;

    private void Start()
    {
        defPos = transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        x = radius * Mathf.Sin(timer * m_maxSpeed);
        y = radius * Mathf.Cos(timer * m_maxSpeed);

        if (timer > 6)
        {
            timer = 0;
        }
        if (timer > 3)
        {
            transform.position = new Vector2(-x + defPos.x, y + defPos.y);
            timer = 0;
        }
        else
        {
            transform.position = new Vector2(x + defPos.x, y + defPos.y);
        }
    }
}
