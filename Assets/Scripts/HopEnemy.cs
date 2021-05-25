using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopEnemy : MonoBehaviour
{
    [SerializeField] float m_life = 10;
    [SerializeField] float m_power = 2;
    [SerializeField] float m_speed = 1;
    [SerializeField] float m_jumpPower = 5;
    [SerializeField] float m_fireInterval = 5;
    [SerializeField] float m_moveinterval = 3;
    [SerializeField] Transform[] m_muzzles = null;
    [SerializeField] GameObject m_bulletPrefab = null;
    [SerializeField] BulletController.MoveDirection m_moveDirection = BulletController.MoveDirection.naname;
    GameObject m_player = null;
    Rigidbody2D m_rb = null;
    bool m_move = false;
    float m_timer;
    Vector2 m_rightJump = new Vector2(0.5f, 1);
    Vector2 m_LeftJump = new Vector2(-0.5f, 1);
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.Find("Player");

        if (m_muzzles == null)
        {
            Debug.Log("Muzzleが設定されてないよ！");
        }
    }


    void Update()
    {
        if (m_player)
        {
            StartCoroutine("Move");
        }

        if (m_bulletPrefab)
        {
            m_timer += Time.deltaTime;
            if (m_timer > m_fireInterval)
            {
                m_timer = 0f;

                foreach (Transform t in m_muzzles)
                {
                    Instantiate(m_bulletPrefab, t.position, Quaternion.identity);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.name == "Player")
            {
                //プレイヤー自身と接触してしまうと後述のBulletControllerを取得する処理でエラーが出るのでそれの対策用
                return;
            }
            BulletController bullet = collision.GetComponent<BulletController>();

            m_life = m_life - bullet.m_power;

            if (m_life <= 0)
            {
                Destroy(gameObject);
            }
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
