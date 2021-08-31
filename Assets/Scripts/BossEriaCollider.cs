using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEriaCollider : MonoBehaviour
{
    private GameObject m_invisibleWall;
    private bool m_isBoss = false;

    void Start()
    {
        m_invisibleWall = gameObject.transform.Find("InvisibleWall").gameObject;
        m_invisibleWall.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_invisibleWall.SetActive(true);
            m_isBoss = true;
        }
    }
}
