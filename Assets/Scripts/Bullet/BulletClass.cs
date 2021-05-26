using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BulletClass : MonoBehaviour
{
    //GameObject m_player;
    //Rigidbody2D m_rb;
    /// <summary> 弾速 </summary>
    [SerializeField] public float m_speed = 1f;
    /// <summary> 弾の攻撃力 </summary>
    [SerializeField] public float m_power = 2f;

    void Start()
    {
        Vector2 v = Vector2.zero;
        //m_player = GameObject.Find("Player");
        //m_rb = GetComponent<Rigidbody2D>();
        /*
        if (m_player)
        {
            if (moveDirection == MoveDirection.aimAtPlayer)
            {
                // 方向の決定
                v = m_player.transform.position - this.transform.position;
            }
            if (moveDirection == MoveDirection.plaeyrX)
            {
                if (m_player.transform.position.x < this.transform.position.x)
                {
                    v = Vector2.left;
                }
                else
                {
                    v = Vector2.right;
                }
            }
            if (moveDirection == MoveDirection.playerY)
            {
                if (m_player.transform.position.y < this.transform.position.y)
                {
                    v = Vector2.down;
                }
                else
                {
                    v = Vector2.up;
                }
            }
            if (moveDirection == MoveDirection.normal)
            {
                if (transform.localScale.x < 0f)
                {
                    v = Vector2.left;
                }
                else
                {
                    v = Vector2.right;
                }
            }
            if (moveDirection == MoveDirection.player)
            {
                
            }
            if (moveDirection == MoveDirection.naname)
            {
                if (transform.localScale.x < 0f)
                {
                    v = new Vector2(-1, 1);
                }
                else
                {
                    v = new Vector2(1, 1);
                }
            }
            Vector2 _v = v.normalized * m_speed;
            // 速度ベクトルをセットする
            m_rb.velocity = _v;
        }
        else
        {
            Destroy(this.gameObject);
        }
        */
    }

    public Vector2 Player(GameObject Player)
    {
        Vector2 v = Vector2.zero;
        if (Player.transform.localScale.x < 0f)
        {
            v = Vector2.left;
        }
        else
        {
            v = Vector2.right;
        }
        Vector2 _v = v.normalized * m_speed;
        return _v;
    }

    public Vector2 AimAtPlayer(GameObject Player)
    {
        Vector2 v = Vector2.zero;
        v = Player.transform.position - this.transform.position;
        Vector2 _v = v.normalized * m_speed;
        return _v;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag != "Untagged" && collision.tag != "Enemy" && collision.tag != "Player")
        //{
        //    Destroy(this.gameObject);
        //}
        if (collision.tag != "Untagged")
        {
            if (this.gameObject.tag == "Enemy" && collision.tag != "Enemy")
            {
                Destroy(this.gameObject);
            }
            if (this.gameObject.tag == "Player" && collision.tag != "Player")
            {
                Destroy(this.gameObject);
            }
        }
    }
}
