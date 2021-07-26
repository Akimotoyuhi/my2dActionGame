using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject m_player = null;
    private Transform[] m_spawnPoint;
    void Start()
    {
        GameObject spawnPoint = GameObject.Find("Spawnpoint");
        m_spawnPoint = new Transform[spawnPoint.transform.childCount];
        for (int i = 0; i < spawnPoint.transform.childCount; i++)
        {
            m_spawnPoint[i] = spawnPoint.transform.GetChild(i);
        }
    }

    void Update()
    {
        
    }
}
