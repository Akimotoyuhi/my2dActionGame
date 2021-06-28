using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : BulletClass
{
    float x;
    float y;
    Vector2 defPos;
    /// <summary>円の大きさ</summary>
    [SerializeField] float radius = 1f;
    Rigidbody2D _rb = null;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        defPos = transform.position;
    }

    private void Update()
    {
        x = radius * Mathf.Sin(Time.time * m_speed);
        y = radius * Mathf.Cos(Time.time * m_speed);

        if (x > 360)
        {
            x = 0;
        }
        if (x > 180)
        {
            _rb.velocity = new Vector2(-x + defPos.x, y + defPos.y);
            //transform.position = new Vector2(-x + defPos.x, y + defPos.y);
        }
        else
        {
            _rb.velocity = new Vector2(x + defPos.x, y + defPos.y);
            //transform.position = new Vector2(x + defPos.x, y + defPos.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
