using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : BulletClass
{
    float x;
    float y;
    Vector2 defPos;
    /// <summary>円の大きさ</summary>
    [SerializeField] float radius = 1f;

    private void Start()
    {
        defPos = transform.position;
    }

    private void Update()
    {
        x = radius * Mathf.Sin(Time.time * m_speed);
        y = radius * Mathf.Cos(Time.time * m_speed);

        transform.position = new Vector2(x + defPos.x, y + defPos.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
