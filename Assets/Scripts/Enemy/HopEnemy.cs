using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopEnemy : Enemy
{
    [SerializeField] float m_sideJump = 0.5f;
    float timer;

    void Update()
    {
        AtPlayer();
        if (m_player)
        {
            timer += Time.deltaTime;
            if (timer > m_moveinterval)
            {
                timer = 0;
                Move();
            }
        }
    }

    public override void Move()
    {
        if (m_isMove || !m_move)
        {
            return;
        }
        m_isMove = true;

        if (m_player.transform.position.x < this.transform.position.x)
        {
            Vector2 jump = new Vector2(-m_sideJump, 1);
            m_rb.AddForce(jump * m_jumpPower, ForceMode2D.Impulse);
        }
        else if (m_player.transform.position.x > this.transform.position.x)
        {
            Vector2 jump = new Vector2(m_sideJump, 1);
            m_rb.AddForce(jump * m_jumpPower, ForceMode2D.Impulse);
        }

        m_isMove = false;
    }
}
