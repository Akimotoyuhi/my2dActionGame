using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : BulletClass
{
    GameObject m_player;
    Rigidbody2D m_rb = null;
    private void Start()
    {
        m_player = GameObject.Find("Player");
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.velocity = AimAtPlayer(m_player);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
