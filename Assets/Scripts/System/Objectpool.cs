using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectpool : MonoBehaviour
{
    [SerializeField] private int m_capasity;
    [SerializeField] private GameObject m_prefab;
    private bool m_isActive = false;
    private Queue<IPoolable<NewBullet>> m_pool;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// ObjectPool作成
    /// 
    /// NOTE:許容量を設定しているが、それは許容量を超えた際にメモリの再割り当てが発生するのを防ぐ為
    /// </summary>
    /// <param name="capasity">許容量</param>
    /// <param name="isFixid"></param>
    private void CreatePool(int capasity, bool isFixid)
    {
        if (m_isActive)
        {
            Debug.LogWarning("Poolは既に生成されています！");
            return;
        }

        if (m_prefab == null)
        {
            Debug.LogError("Prefabがセットされていません！");
            return;
        }

        m_capasity = capasity;
        m_pool = new Queue<IPoolable<NewBullet>>(capasity);
        for (int i = 0; i < capasity; i++)
        {
            NewBullet obj = Instantiate(m_prefab, transform).GetComponent<NewBullet>();
            obj.gameObject.SetActive(false);
            m_pool.Enqueue(obj);
        }
    }
}

/// <summary>
/// プールさせるオブジェクトには、このインターフェースの実装を強制する
/// EntityはIPoolableを実装するクラスのインスタンスを返させる
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPoolable<T> where T : MonoBehaviour
{
    T Entity { get; }

    void OnReleased(); //生成時
    void OnCatched(); //破棄時
}