using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bakuhatu : MonoBehaviour
{
    private float m_destroyTime = 0.1f;
    private float m_timer;
    [System.NonSerialized] public int m_power;

    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer < m_destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}
