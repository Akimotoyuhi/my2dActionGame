﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPig : Enemy
{

    void Update()
    {
        AtPlayer();
        Move();
    }

    private void Move()
    {
        if (m_player)
        {
            m_moveTimer += Time.deltaTime;
            if (m_moveTimer > m_moveinterval)
            {
                if (m_player.transform.position.x < this.transform.position.x)
                {
                    for (float i = 0; i < 3; i += Time.deltaTime)
                    {
                        m_rb.velocity = new Vector2(-m_speed, m_rb.velocity.y);
                    }
                }
                else
                {
                    for (float i = 0; i < 3; i += Time.deltaTime)
                    {
                        m_rb.velocity = new Vector2(m_speed, m_rb.velocity.y);
                    }
                }
                m_moveTimer = 0;
            }
        }
    }
}