using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_player = null;
    [System.NonSerialized] public Vector2 m_spawnPoint = new Vector2(-11f, -2.5f);
    [SerializeField] private bool isTest = false;
    [SerializeField] private GameObject m_enemySpawnpoint;
    void Start()
    {
        m_enemySpawnpoint = GameObject.Find("EnemiesSpawnpoints");
        if (!isTest)
        {
            PlayerSpawn();
        }
    }

    public void EnemySpawning()
    {
        for (int i = 0; i < m_enemySpawnpoint.transform.childCount; i++)
        {
            EnemySpawnPoint e = m_enemySpawnpoint.transform.GetChild(i).GetComponent<EnemySpawnPoint>();
            e.EnemySpawn();
        }
    }

    public void PlayerDead()
    {
        Invoke("PlayerSpawn", 3f);
    }

    public void PlayerSpawn()
    {
        Instantiate(m_player, m_spawnPoint, Quaternion.identity);
    }
}
