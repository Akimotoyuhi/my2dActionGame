using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    /// <summary>発射する弾の速度(最大速度)</summary>
    [SerializeField] public float m_maxSpeed = 1f;
    /// <summary>発射する弾の速度(最低速度)</summary>
    [SerializeField] public float m_minSpeed = 1f;
    /// <summary>弾の攻撃力</summary>
    [SerializeField] public int m_bulletPower = 1;
    /// <summary>弾を発射する間隔（秒）</summary>
    [SerializeField] public float m_fireInterval = 1f;
    /// <summary>連射回数</summary>
    [SerializeField] public int m_barrage = 1;
    /// <summary>連射間隔</summary>
    [SerializeField] public float m_barrageTime = 1f;
    /// <summary>弾の発射方向</summary>
    [SerializeField] public Vector2 m_vector = Vector2.zero;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] private GameObject m_bulletPrefab = null;
    /// <summary>弾の色</summary>
    [SerializeField] private Color m_color;
    /// <summary>弾幕の性質</summary>
    [SerializeField] public Pattern _pattenName = Pattern.Aim_at_Player;
    /// <summary>発射間隔に使うタイマー</summary>
    [SerializeField] public float m_timer = 0;
    /// <summary>プレイヤーの位置によって発射向きを変更するかどうか</summary>
    [SerializeField] public bool m_changeDirection = false;
    /// <summary>着弾点を指定したい時にどうぞ</summary>
    //[SerializeField] public bool m_selectTarget = false;
    /// <summary>現在が弾幕中かを判定する</summary>
    [System.NonSerialized] public bool m_isBullet = false;
    [System.NonSerialized] public GameObject m_player;
    private BulletBase m_bullet;

    public enum Pattern
    {
        Aim_at_Player,
        Designation
    }

    public Vector2 SetDirection()
    {
        if (m_player.transform.position.x < this.gameObject.transform.position.x)
        {
            return Vector2.left;
        }
        else
        {
            return Vector2.right;
        }
    }

    public void InstantiateAndColor(Vector2 v)
    {
        var t = Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);
        if (t.GetComponent<SpriteRenderer>())
        {
            t.GetComponent<SpriteRenderer>().color = m_color;
        }
        m_bullet = t.GetComponent<BulletBase>();
        if (m_bullet)
        {
            m_bullet.m_maxSpeed = m_maxSpeed;
            m_bullet.m_minSpeed = m_minSpeed;
            m_bullet.m_power = m_bulletPower;
            m_bullet.m_velo = v;
        }
    }

    /// <summary>
    /// 自機狙いの単発の弾をランダムな速度で撃つ
    /// </summary>
    public void Single_RandomSpeed()
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            m_timer = 0f;

            Vector2 v = Vector2.down;
            v.Normalize();
            float speed = Random.Range(m_minSpeed, m_maxSpeed);
            v *= speed;
            InstantiateAndColor(v);
        }
    }

    public void Single_RandomSpeed(Vector2 vec)
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            m_timer = 0f;

            Vector2 v = vec;
            v.Normalize();
            float speed = Random.Range(m_minSpeed, m_maxSpeed);
            v *= speed;
            InstantiateAndColor(v);
        }
    }

    public void Installation()
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            m_timer = 0;

            InstantiateAndColor(Vector2.zero);
        }
    }
}