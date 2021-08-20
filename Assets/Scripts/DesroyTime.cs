using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesroyTime : MonoBehaviour
{
    private float m_timer = 0;
    [SerializeField] private float m_destroyTime = 0;

    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
