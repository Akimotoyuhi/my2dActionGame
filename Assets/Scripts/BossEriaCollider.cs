using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEriaCollider : MonoBehaviour
{
    private GameObject m_invisibleWall;
    [SerializeField] private GameObject m_bossPrefab;
    [SerializeField] private Transform m_bossPos;
    [SerializeField] private GameObject m_bossHpGage;
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
            if (!m_isBoss)
            {
                IsBoss();
            }
        }
    }

    private void IsBoss()
    {
        m_isBoss = true;
        m_invisibleWall.SetActive(true);
        m_bossHpGage.SetActive(true);
        Instantiate(m_bossPrefab, m_bossPos.position, Quaternion.identity);
    }
}
