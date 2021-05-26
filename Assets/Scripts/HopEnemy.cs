using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopEnemy : Enemy
{
    //float m_life = 10;
    //float m_power;
    //float m_speed;
    //float m_jumpPower;
    //[SerializeField] float m_fireInterval = 5;
    //float m_moveinterval;
    //[SerializeField] Transform[] m_muzzles = null;
    //[SerializeField] GameObject m_bulletPrefab = null;
    //[SerializeField] BulletController.MoveDirection m_moveDirection = BulletController.MoveDirection.naname;
    GameObject m_player = null;
    Rigidbody2D m_rb = null;
    bool m_move = false;
    //float m_timer;
    Vector2 m_rightJump = new Vector2(0.5f, 1);
    Vector2 m_LeftJump = new Vector2(-0.5f, 1);
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.Find("Player");
    }


    void Update()
    {
        AtPlayer(m_player);
        if (m_player)
        {
            StartCoroutine("Move");
        }
    }

    IEnumerator Move()
    {
        float xSpeed = 0f;
        if (m_move)
        {
            yield break;
        }

        m_move = true;

        if (m_player.transform.position.x < this.gameObject.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            m_rb.AddForce(m_LeftJump * m_jumpPower, ForceMode2D.Impulse);
            xSpeed = -m_speed;
            yield return new WaitForSeconds(m_moveinterval);
        }
        else if (m_player.transform.position.x > this.gameObject.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            m_rb.AddForce(m_rightJump * m_jumpPower, ForceMode2D.Impulse);
            xSpeed = m_speed;
            yield return new WaitForSeconds(m_moveinterval);
        }
        else
        {
            xSpeed = 0f;
        }

        m_move = false;
    }
}
