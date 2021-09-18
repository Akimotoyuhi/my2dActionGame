using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class huttpbasu : MonoBehaviour
{
    private Rigidbody2D m_rb;
    void Start()
    {
        Vector2 v = new Vector2(Random.Range(-1f, 1f), 1);
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.AddForce(v * 8, ForceMode2D.Impulse);
    }

    void Update()
    {

    }
}
