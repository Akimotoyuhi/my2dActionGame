using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject m_enemyPrefab;
    void Start()
    {

    }

    /// <summary>
    /// このオブジェクトに設定された敵を出現させる
    /// </summary>
    public void EnemySpawn()
    {
        if (gameObject.transform.childCount > 0)
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        GameObject g = Instantiate(m_enemyPrefab, this.gameObject.transform.position, Quaternion.identity, this.transform);
    }
}
