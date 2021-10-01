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
    [Header("基本項目")]
    /// <summary>弾の種類</summary>
    private Types[] m_types;
    /// <summary>撃つ弾のそれ</summary>
    [SerializeField] private ShotType m_shotType;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] private GameObject m_bulletPrefab;
    /// <summary>弾の色</summary>
    [SerializeField] private Color m_color;
    [Header("速度関係")]
    /// <summary>弾の最終速度</summary>
    [SerializeField] private float m_endSpeed;
    /// <summary>弾の初速</summary>
    [SerializeField] private float m_startSpeed;
    /// <summary>弾の加速度</summary>
    [SerializeField] private float m_speedUp;
    [Header("発射間隔とパラメーター")]
    /// <summary>弾の攻撃力</summary>
    [SerializeField] private int m_bulletPower = 1;
    /// <summary>弾が消えるまでの時間</summary>
    [SerializeField] private float m_lifeTime = 10f;
    /// <summary>発射間隔</summary>
    [SerializeField] private float m_fireInterval = 1;
    [Header("カーブ関係")]
    /// <summary>弾のカーブのすごさ</summary>
    [SerializeField] private float m_curve;
    [Header("角度関係")]
    /// <summary>発射角度</summary>
    [SerializeField] private float m_zAngle;
    /// <summary>回転速度</summary>
    [SerializeField] private float m_spinSpeed;
    /// <summary>回転が逆になる弾数</summary>
    [SerializeField] private int m_zAngleTurn;
    [Header("特殊設定項目")]
    /// <summary>Way発射数</summary>
    [SerializeField] private int m_waynum = 1;
    /// <summary>角度</summary>
    [SerializeField] private float m_angle;
    [Header("追加オプション")]
    /// <summary>自機狙いかどうか</summary>
    [SerializeField] private bool m_isPlayer = false;
    /// <summary>速度を変えるかどうか</summary>
    [SerializeField] private bool m_isSpeedChange = false;
    /// <summary>プレイヤーのｘ位置によって向きを逆にするか</summary>
    [SerializeField] private bool m_isSetPlayerXpos = false;
    /// <summary>敵に複数のパターンを設定する時はこれをtrueにして敵側からfalseにしてくれ</summary>
    [SerializeField] private bool m_isStop = false;
    /// <summary>発射した弾をこのオブジェクトの子とするか</summary>
    [SerializeField] private bool m_isParent = false;
    [Header("射出位置関係")]
    /// <summary>発射位置</summary>
    [SerializeField] private Transform m_tra;
    /// <summary>発射位置の誤差</summary>
    [SerializeField] private float m_posDifference;
    private float m_z;
    private float m_timer = 99;
    private int m_shotCount = 0;
    private bool m_isTurm = false;
    private GameObject m_player;
    private Vector2 m_vec;

    void Start()
    {
        m_player = GameObject.FindWithTag("Player");
        SetTypes();
        transform.rotation = Quaternion.Euler(0, 0, m_zAngle);
        m_z = m_zAngle;
    }

    void Update()
    {
        if (!m_isStop)
        {
            ShotTrigger();
        }
    }

    public void ShotTrigger()
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_fireInterval)
        {
            m_timer = 0;
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
        SetAngle();
        Shot();
    }

    /// <summary>
    /// 角度と弾数を指定して扇形に弾を出す
    /// </summary>
    private void Nway()
    {
        SetAngle();
        transform.Rotate(new Vector3(0, 0, (m_angle / m_waynum) * (-m_waynum / 2)));
        for (int i = 0; i < m_waynum; i++)
        {
            Shot();
            transform.Rotate(new Vector3(0, 0, m_angle / m_waynum));
        }
        transform.rotation = Quaternion.Euler(0, 0, m_zAngle);
    }

    /// <summary>
    /// 回転しながら弾を出す
    /// </summary>
    private void Spin()
    {
        for (int i = 0; i < m_waynum; i++)
        {
            if (!m_isTurm)
            {
                transform.Rotate(new Vector3(0, 0, m_spinSpeed));
                Shot();
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, -m_spinSpeed));
                Shot();
            }
        }
        if (m_zAngleTurn == 0)
        {
            return;
        }
        m_shotCount++;
        if (m_shotCount >= m_zAngleTurn)
        {
            m_shotCount = 0;
            if (!m_isTurm)
            {
                m_isTurm = true;
            }
            else if (m_isTurm)
            {
                m_isTurm = false;
            }
        }
    }

    /// <summary>
    /// 弾の発射
    /// </summary>
    private void Shot()
    {
        if (!m_player) { m_player = GameObject.FindWithTag("Player"); }
        if (m_isSetPlayerXpos)
        {
            //自分から見てプレイヤーが左右どっちかにいるかを判別して自分の向く方向を変える
            if (m_player.transform.position.x < this.gameObject.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, m_zAngle);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, -m_zAngle);
            }
        }

        if (!m_tra) { m_tra = this.transform; }
        m_vec = new Vector2(Random.Range(m_tra.position.x - m_posDifference, m_tra.position.x + m_posDifference), Random.Range(m_tra.position.y - m_posDifference, m_tra.position.y + m_posDifference));
        var t = Instantiate(m_bulletPrefab, m_vec, this.transform.rotation);
        if (m_isParent) { t.transform.parent = this.transform; }
        if (t.GetComponent<SpriteRenderer>())
        {
            t.GetComponent<SpriteRenderer>().color = m_color;
        }
        NewBullet m_bullet = t.GetComponent<NewBullet>();
        if (m_bullet)
        {
            m_bullet.SetParameter(m_endSpeed, m_startSpeed, m_speedUp, m_curve, m_bulletPower, m_lifeTime);
        }
    }

    private void SetAngle()
    {
        if (m_isPlayer)
        {
            if (!m_player) { m_player = GameObject.FindWithTag("Player"); }
            Vector2 v = m_player.transform.position - this.transform.position;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        else
        {
            //transform.rotation = Quaternion.Euler(0, 0, m_zAngle);
        }
    }
}
