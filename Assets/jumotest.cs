using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumotest : MonoBehaviour
{
    private float m_yspd = 9;
    private float m_grv = 0.9f;
    private float m_time = 0;
    private float m_y;
    [SerializeField] private IsGrounded m_ground;
    private bool m_isground;

    void Start()
    {
        m_isground = m_ground.IsGrouded();
    }

    void Update()
    {
        
    }

    private void Jump()
    {
        if (m_isground)
        {
            return;
        }

        //m_y = 0.5 * m_grv * m_time * m_time - m_yspd + 
    }
}
