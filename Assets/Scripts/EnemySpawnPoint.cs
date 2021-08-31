using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject m_enemyPrefab;

    /// <summary>
    /// インスペクターで設定された敵を出現させる
    /// </summary>
    public void EnemySpawn()
    {
        if (gameObject.transform.childCount > 0)
        {
            EnemyDestroy();
        }
        GameObject g = Instantiate(m_enemyPrefab, this.gameObject.transform.position, Quaternion.identity, this.transform);
    }

    /// <summary>
    /// 現在出現中の敵を削除する
    /// </summary>
    public void EnemyDestroy()
    {
        if (gameObject.transform.childCount == 0)
        {
            return;
        }
        Destroy(gameObject.transform.GetChild(0).gameObject);
    }
}
