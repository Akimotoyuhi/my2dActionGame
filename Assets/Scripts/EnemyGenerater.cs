using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{
    [SerializeField] private GameObject m_enemyPrefab;
    private GameObject m_seveObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (m_seveObject)
            {
                Destroy(m_seveObject);
            }
            m_seveObject = Instantiate(m_enemyPrefab, this.gameObject.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_seveObject)
        {
            Destroy(m_seveObject);
        }
    }
}
