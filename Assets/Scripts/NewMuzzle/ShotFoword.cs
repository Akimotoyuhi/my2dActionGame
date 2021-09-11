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
    [SerializeField] private int m_bulletPower = 1;
    /// <summary>発射間隔</summary>
    [SerializeField] private float m_fireInterval = 1;
    /// <summary>弾のカーブのすごさ</summary>
    [SerializeField] private float m_curve;
    /// <summary>発射角度</summary>
    [SerializeField] private float m_zAngle;
    /// <summary>回転速度</summary>
    [SerializeField] private float m_spinSpeed;
    /// <summary>Way発射数</summary>
    [SerializeField] private int m_waynum = 1;
    /// <summary>角度</summary>
    [SerializeField] private float m_angle;
    /// <summary>自機狙いかどうか</summary>
    [SerializeField] private bool m_isPlayer = false;
    /// <summary>速度を変えるかどうか</summary>
    [SerializeField] private bool m_isSpeedChange = false;
    /// <summary>プレイヤーのｘ位置によって向きを逆にするか</summary>
    [SerializeField] private bool m_isSetPlayerXpos = false;
    /// <summary>敵に複数のパターンを設定する時はこれをtrueにして敵側からfalseにしてくれ</summary>
    [SerializeField] private bool m_isStop = false;
    /// <summary>発射位置</summary>
    [SerializeField] private Transform m_tra;
    private float m_z;
    private float m_timer = 99;
    private GameObject m_player;

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
    /// 一回だけ撃ちたい時の関数（FireIntarvalを無視できる）
    /// </summary>
    public void OneShot()
    {
        m_types[(int)m_shotType]();
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
    /// 弾の発射を止める時に呼ばれる
    /// </summary>
    public void StopEnable()
    {
        m_isStop = true;
        m_zAngle = m_z;
    }

    /// <summary>
    /// 弾の発射を始める時に呼ばれる
    /// </summary>
    public void StopDisable()
    {
        m_isStop = false;
        m_z = m_zAngle;
    }

    /// <summary>
    /// 単発弾
    /// </summary>
    private void Single()
    {
        if (m_isPlayer)
        {
            //自機狙い ※参考サイト https://nekojara.city/unity-look-at
            if (!m_player) { GameObject.FindWithTag("Player"); }
            var v = m_player.transform.position - this.transform.position;
            var rotation = Quaternion.LookRotation(v, Vector3.up);
            var offset = Quaternion.FromToRotation(m_player.transform.position, Vector3.forward);
            var lookRotation = rotation * offset;
            transform.rotation = lookRotation;
        }
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
        transform.rotation = Quaternion.Euler(0, 0, m_zAngle);
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
        var t = Instantiate(m_bulletPrefab, m_tra.position, this.transform.rotation);
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
