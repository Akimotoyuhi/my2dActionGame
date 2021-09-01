using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBase : Enemy
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 二つの数値の割合(%)を返す
    /// </summary>
    /// <param name="now">現在の数値</param>
    /// <param name="max">最大の数値</param>
    /// <returns>二つの数値の割合</returns>
    public float Percent(float now, float max) { return (now / max) * 100; }
}
