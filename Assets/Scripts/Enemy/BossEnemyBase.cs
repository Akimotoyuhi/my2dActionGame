using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBase : Enemy
{
    public delegate void Actions();
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetOnShot(Single[] singles, Nway[] nways, Uzumaki[] uzumakis)
    {
        for (int i = 0; i < singles.Length; i++)
        {
            singles[i].OnShot();
        }
        for (int i = 0; i < nways.Length; i++)
        {
            nways[i].OnShot();
        }
        for (int i = 0; i < uzumakis.Length; i++)
        {
            uzumakis[i].OnShot();
        }
    }

    /// <summary>
    /// 二つの数値の割合(%)を返す
    /// </summary>
    /// <param name="now">現在の数値</param>
    /// <param name="max">最大の数値</param>
    /// <returns>二つの数値の割合</returns>
    public float Percent(float now, float max) { return (now / max) * 100; }
}
