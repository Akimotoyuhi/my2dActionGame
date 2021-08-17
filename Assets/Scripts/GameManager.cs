using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_player = null;
    [System.NonSerialized] public Vector2 m_spawnPoint = new Vector2(-11f, -2.5f);
    [SerializeField] private bool isTest = false;
    void Start()
    {
        GameObject spawnPoint = GameObject.Find("Spawnpoint");

        if (!isTest)
        {
            PlayerSpawn();
        }
    }

    void Update()
    {
        
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
