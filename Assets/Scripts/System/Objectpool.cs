using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectpool<T> where T : Object, IPoolable
{
    private T m_baseObj = null;
    private Transform m_parent = null;
    /// <summary>プール</summary>
    private List<T> m_pool = new List<T>();
    private int m_index = 0;

    public void SetBaseObj(T obj, Transform parent)
    {
        m_baseObj = obj;
        m_parent = parent;
    }

    /// <summary>プールに追加</summary>
    /// <param name="obj">プールするオブジェクト</param>
    public void Pooling(T obj)
    {
        obj.DisactiveForInstantiate();
        m_pool.Add(obj);
    }

    /// <summary>
    /// プールのサイズ設定
    /// プール内の全てのオブジェクトが使われていた場合は新たに追加する（？）
    /// </summary>
    /// <param name="size"></param>
    public void SetCapasity(int size)
    {
        if (size < m_pool.Count) return;

        for (int i = m_pool.Count - 1; i < size; i++)
        {
            T obj = default(T);
            if (m_parent)
            {
                obj = GameObject.Instantiate(m_baseObj, m_parent);
            }
            else
            {
                obj = GameObject.Instantiate(m_baseObj);
            }
            obj.DisactiveForInstantiate();
            m_pool.Add(obj);
        }
    }

    public T Instansiate()
    {
        T ret = null;
        for (int i = 0; i < m_pool.Count; i++)
        {
            int index = (m_index + i) % m_pool.Count;
            if (m_pool[index].IsActive) continue;

            m_pool[index].Create();
            ret = m_pool[index];
            break;
        }
        return ret;
    }
}

public interface IPoolable
{
    bool IsActive { get; }
    void DisactiveForInstantiate();
    void Create(); //生成時
    void Detroy(); //破棄時
}