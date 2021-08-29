using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : BulletBase
{
    GameObject m_player;
    //private Rigidbody2D m_rb;
    void Start()
    {
        m_player = GameObject.Find("Player");
        //m_rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
