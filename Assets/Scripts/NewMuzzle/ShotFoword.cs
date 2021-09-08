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
    [SerializeField] private float m_maxSpeed = 1;
    /// <summary>弾の最低速度（初速度）</summary>
    [SerializeField] private float m_minSpeed = 1;
    /// <summary>弾の攻撃力</summary>
    [SerializeField] private int m_bulletPower = 1;
    /// <summary>発射間隔</summary>
    [SerializeField] private float m_fireInterval = 1;
    /// <summary>弾のカーブのすごさ</summary>
    [SerializeField] private float m_curve = 0;
    /// <summary>発射角度</summary>
    [SerializeField] private float m_posz;
    /// <summary>回転速度</summary>
    [SerializeField] private float m_spinSpeed = 0;
    /// <summary>Way発射数</summary>
    [SerializeField] private int m_waynum = 0;
    /// <summary>角度</summary>
    [SerializeField] private float m_angle = 0;
    /// <summary>自機狙いかどうか</summary>
    [SerializeField] private bool m_isPlayer = false;
    /// <summary>速度を変えるかどうか</summary>
    [SerializeField] private bool m_isSpeedChange = false;
    [SerializeField] private bool m_isSetPlayerXpos = false;
    private bool now = false;
    private float timer = 99;
    private GameObject m_player;

    void Start()
    {
        m_player = GameObject.FindWithTag("Player");
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
        if (!m_player) { m_player = GameObject.FindWithTag("Player"); }
        if (m_isSetPlayerXpos)
        {
            if (m_player.transform.position.x < this.gameObject.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, m_posz);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, -m_posz);
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, m_posz);
        }
        var t = Instantiate(m_bulletPrefab, this.transform.position, this.transform.rotation);
        if (t.GetComponent<SpriteRenderer>())
        {
            t.GetComponent<SpriteRenderer>().color = m_color;
        }
        NewBullet m_bullet = t.GetComponent<NewBullet>();
        if (m_bullet)
        {
            m_bullet.SetParameter(m_maxSpeed, m_minSpeed, m_curve, m_bulletPower);
        }
    }
}
