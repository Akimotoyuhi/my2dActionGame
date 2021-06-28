using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopEnemy : Enemy
{
    GameObject m_player = null;
    Rigidbody2D m_rb = null;
    bool m_isMove = false;
    [SerializeField] float m_sideJump = 0.5f;
    float timer;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.Find("Player");
    }


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

    private void Move()
    {
        if (m_isMove)
        {
            return;
        }
        m_isMove = true;

        if (m_player.transform.position.x < this.transform.position.x)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            Vector2 jump = new Vector2(-m_sideJump, 1);
            m_rb.AddForce(jump * m_jumpPower, ForceMode2D.Impulse);
        }
        else if (m_player.transform.position.x > this.transform.position.x)
        {
            //transform.localScale = new Vector3(1, 1, 1);
            Vector2 jump = new Vector2(m_sideJump, 1);
            m_rb.AddForce(jump * m_jumpPower, ForceMode2D.Impulse);
        }

        m_isMove = false;
    }
}
