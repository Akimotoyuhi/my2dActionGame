using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPig : Enemy
{
    private void Start()
    {
        AnimSetUp();
    }

    void Update()
    {
        if (m_player)
        {
            m_moveTimer += Time.deltaTime;
            if (m_moveTimer > m_moveinterval)
            {
                m_moveTimer = 0;
                Move();
            }
        }
        else
        {
            m_player = GameObject.FindWithTag("Player");
        }
    }

    public override void Move()
    {
        base.Move();
        
        m_isMove = true;
        m_anim.SetBool("isMove", m_isMove);
        float xspeed = 0;
        
        AtPlayer();
        if (m_player.transform.position.x < this.transform.position.x)
        {
            xspeed = -m_speed;
        }
        else
        {
            xspeed = m_speed;
        }
        m_rb.velocity = new Vector2(xspeed, m_rb.velocity.y);
        m_isMove = false;
        m_anim.SetBool("isMove", m_isMove);
    }
}
