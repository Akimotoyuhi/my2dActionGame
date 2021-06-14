using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BulletClass : MonoBehaviour
{
    /// <summary> 弾速 </summary>
    [SerializeField] public float m_speed = 1f;
    /// <summary> 弾の攻撃力 </summary>
    [SerializeField] public float m_power = 2f;

    public Vector2 Player(GameObject Player)
    {
        Vector2 v = Vector2.zero;
        if (Player.transform.localScale.x < 0f)
        {
            v = Vector2.left;
        }
        else
        {
            v = Vector2.right;
        }
        Vector2 _v = v.normalized * m_speed;
        return _v;
    }

    public Vector2 AimAtPlayer(GameObject Player)
    {
        Vector2 v = Vector2.zero;
        v = Player.transform.position - this.transform.position;
        Vector2 _v = v.normalized * m_speed;
        return _v;
    }
}