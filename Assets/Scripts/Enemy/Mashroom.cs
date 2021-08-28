using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mashroom : Enemy
{
    void Start()
    {
        FullSetUp();
    }

    void Update()
    {
        AtPlayer();
        if (m_player)
        {
            m_moveTimer += Time.deltaTime;
            if (m_moveTimer > m_moveinterval)
            {
                Move();
            }
        }
    }

    public override void Move()
    {
        base.Move();

        m_isMove = true;

        AtPlayer();
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

        m_isMove = false;
    }
}
