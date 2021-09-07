using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFoword : MonoBehaviour
{
    private enum ShotType
    {
        Single = 0,
        Nway = 1,
        Spin = 2
    }
    private delegate void Types();
    /// <summary>弾の種類</summary>
    private Types[] m_types;
    /// <summary>撃つ弾のそれ</summary>
    [SerializeField] private ShotType m_shotType;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] private GameObject m_bulletPrefab;
    /// <summary>弾の色</summary>
    [SerializeField] private Color m_color;
    /// <summary>弾の最高速度</summary>
    [SerializeField] private float m_maxSpeed;
    /// <summary>弾の最低速度（初速度）</summary>
    [SerializeField] private float m_minSpeed;
    /// <summary>弾の攻撃力</summary>
    [SerializeField] private int m_bulletPower;
    /// <summary>発射間隔</summary>
    [SerializeField] private float m_fireInterval;
    /// <summary>自機狙いかどうか</summary>
    [SerializeField] private bool m_isPlayer = false;
    /// <summary>自機狙いかどうか</summary>
    [SerializeField] private bool m_isSpeedChange = false;
    /// <summary>Spinメソッドでの回転速度</summary>
    [SerializeField] private float m_spinSpeed;
    /// <summary>Nwayメソッドでの発射数</summary>
    [SerializeField] private int m_waynum;
    /// <summary>Nwayメソッドでの発射角度</summary>
    [SerializeField] private float m_angle;
    private bool now = false;
    private float timer = 0;

    void Start()
    {
        SetTypes();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > m_fireInterval)
        {
            timer = 0;
            m_types[(int)m_shotType]();
        }
    }

    /// <summary>
    /// 攻撃パターンを配列に入れる
    /// </summary>
    private void SetTypes()
    {
        m_types = new Types[3];
        m_types[(int)ShotType.Single] = Single;
        m_types[(int)ShotType.Nway] = Nway;
        m_types[(int)ShotType.Spin] = Spin;
    }

    /// <summary>
    /// 単発弾
    /// </summary>
    private void Single()
    {
        Shot();
    }

    /// <summary>
    /// 角度と弾数を指定して扇形に弾を出す
    /// </summary>
    private void Nway()
    {
        transform.Rotate(new Vector3(0, 0, (m_angle / m_waynum) * (-m_waynum / 2)));
        for (int i = 0; i < m_waynum; i++)
        {
            Shot();
            transform.Rotate(new Vector3(0, 0, m_angle / m_waynum));
        }
        transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// 回転しながら弾を出す
    /// </summary>
    private void Spin()
    {
        for (int i = 0; i < m_waynum; i++)
        {
            transform.Rotate(new Vector3(0, 0, m_spinSpeed));
            Shot();
        }
    }

    private void Shot()
    {
        var t = Instantiate(m_bulletPrefab, this.transform.position, this.transform.rotation);
        if (t.GetComponent<SpriteRenderer>())
        {
            t.GetComponent<SpriteRenderer>().color = m_color;
        }
        NewBullet m_bullet = t.GetComponent<NewBullet>();
        if (m_bullet)
        {
            //m_bullet.m_maxSpeed = m_maxSpeed;
            m_bullet.m_minSpeed = m_minSpeed;
            m_bullet.m_power = m_bulletPower;
        }
    }
}
