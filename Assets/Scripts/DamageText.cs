using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private float y;
    [System.NonSerialized] public Vector2 m_vec;

    private void Start()
    {
        y = m_vec.y;
    }

    void Update()
    {
        y += 0.01f;
        transform.position = new Vector2(m_vec.x, y);
    }
}
