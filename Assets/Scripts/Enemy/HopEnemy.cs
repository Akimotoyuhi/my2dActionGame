using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopEnemy : Enemy
{
    GameObject m_player = null;
    Rigidbody2D m_rb = null;
    bool m_isMove = false;
    [SerializeField] float m_sideJump = 0.5f;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.Find("Player");
    }


    void Update()
    {
        AtPlayer(m_player);
        Single();
        if (m_player)
        {
            StartCoroutine("Move");
        }
    }

    IEnumerator Move()
    {
        if (m_isMove)
        {
            yield break;
        }

        m_isMove = true;
        float xSpeed = 0f;

        if (m_player.transform.position.x < this.gameObject.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Vector2 jump = new Vector2(-m_sideJump, 1);
            m_rb.AddForce(jump * m_jumpPower, ForceMode2D.Impulse);
            xSpeed = -m_speed;
            yield return new WaitForSeconds(m_moveinterval);
        }
        else if (m_player.transform.position.x > this.gameObject.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Vector2 jump = new Vector2(m_sideJump, 1);
            m_rb.AddForce(jump * m_jumpPower, ForceMode2D.Impulse);
            xSpeed = m_speed;
            yield return new WaitForSeconds(m_moveinterval);
        }

        m_isMove = false;
    }
}
