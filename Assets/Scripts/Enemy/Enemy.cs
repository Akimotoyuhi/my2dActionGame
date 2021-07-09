using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    /// <summary>最大体力</summary>
    [SerializeField] public int m_maxLife = 10;
    /// <summary>体力</summary>
    [SerializeField] public int m_life = 10;
    /// <summary>攻撃力</summary>
    [SerializeField] public int m_power = 2;
    /// <summary>移動速度</summary>
    [SerializeField] public float m_speed = 1;
    /// <summary>ジャンプ力</summary>
    [SerializeField] public float m_jumpPower = 5;
    /// <summary>移動間隔</summary>
    [SerializeField] public float m_moveinterval = 3;

    /// <summary>
    /// プレイヤーの位置によって自身の身体の向きを変える
    /// </summary>
    public void AtPlayer()
    {
        GameObject player = GameObject.Find("Player");
        if (player)
        {
            if (player.transform.position.x < this.gameObject.transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            BulletBase bullet = collision.GetComponent<BulletBase>();
            m_life -= bullet.m_power;
        }

        if (m_life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
