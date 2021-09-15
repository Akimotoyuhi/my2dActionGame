using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEriaCollider : MonoBehaviour
{
    [SerializeField] private GameObject m_invisibleWall;
    [SerializeField] private GameObject m_bossPrefab;
    [SerializeField] private Transform m_bossPos;
    [SerializeField] private GameObject m_bossHpGage;
    private bool m_isDefeat;
    public bool m_isBoss = false;
    private GameManager m_gameManager;

    void Start()
    {
        m_invisibleWall.SetActive(false);
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!m_isBoss && !m_isDefeat)
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
        GameObject g = Instantiate(m_bossPrefab, m_bossPos.position, Quaternion.identity);
        g.transform.parent = this.transform;
    }

    public void EndBoss()
    {
        m_isBoss = false;
        m_invisibleWall.SetActive(false);
        m_bossHpGage.SetActive(false);
        m_isDefeat = true;
    }
}
