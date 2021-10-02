using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemyBase : MonoBehaviour, IDamage
{
    /// <summary>最大体力</summary>
    [SerializeField] public int m_maxLife = 1;
    /// <summary>体力</summary>
    [SerializeField] public int m_life = 1;
    /// <summary>攻撃力</summary>
    [SerializeField] public int m_power = 1;
    /// <summary>移動速度</summary>
    [SerializeField] public float m_speed = 1;
    /// <summary>ジャンプ力</summary>
    [SerializeField] public float m_jumpPower = 1;
    /// <summary>移動間隔</summary>
    [SerializeField] public float m_moveInterval = 1;
    /// <summary>発射間隔</summary>
    [SerializeField] public float m_shotinterval = 1;
    /// <summary>移動間隔用タイマー</summary>
    [System.NonSerialized] public float m_moveTimer;
    /// <summary>発射間隔用タイマー</summary>
    [System.NonSerialized] public float m_shotTimer;
    /// <summary>現在移動中か</summary>
    [System.NonSerialized] public bool m_isMove = false;
    /// <summary>ダメージ表示用キャンバス</summary>
    [SerializeField] public GameObject m_damagePrefab;
    /// <summary>弾撃つ場所</summary>
    [SerializeField] public Transform[] m_position;
    [System.NonSerialized] public GameObject m_player;
    [System.NonSerialized] public Rigidbody2D m_rb;
    [System.NonSerialized] public Animator m_anim;
    [System.NonSerialized] public GameObject m_canvas;
    [System.NonSerialized] public Slider m_hpSlider;

    public delegate IEnumerator Actions();

    public void SetUp()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.FindWithTag("Player");
        m_canvas = GameObject.Find("Canvas");
        m_hpSlider = m_canvas.transform.Find("BossHPgage").GetComponent<Slider>();
        m_hpSlider.maxValue = m_maxLife;
        m_hpSlider.value = m_life;
    }

    public void AnimSetUp()
    {
        SetUp();
        m_anim = GetComponent<Animator>();
    }

    /// <summary>
    /// 配列に入っている全てのMuzzleから弾を発射する
    /// </summary>
    /// <param name="muzzle">発射する配列</param>
    /// <param name="pos">発射場所</param>
    /// <param name="sec">発射秒数</param>
    /// <returns></returns>
    public IEnumerator AllShot(GameObject[] muzzle, Transform pos , float sec = 0)
    {
        GameObject[] g = new GameObject[muzzle.Length];
        for (int i = 0; i < muzzle.Length; i++)
        {
            g[i] = Instantiate(muzzle[i], pos.position, Quaternion.identity);
            g[i].transform.parent = this.transform;
        }
        yield return new WaitForSeconds(sec);
        for (int i = 0; i < muzzle.Length; i++)
        {
            Destroy(g[i]);
        }
    }

    /// <summary>
    /// Muzzleからしばらくの間弾を発射する
    /// </summary>
    /// <param name="muzzle">発射するMuzzle</param>
    /// /// <param name="pos">発射場所</param>
    /// <param name="sec">発射秒数</param>
    /// <returns></returns>
    public IEnumerator TyottoShot(GameObject muzzle, Transform pos, float sec = 0)
    {
        GameObject g = Instantiate(muzzle, pos.position, Quaternion.identity);
        g.transform.parent = this.transform;
        yield return new WaitForSeconds(sec);
        Destroy(g);
    }

    /// <summary>
    /// Muzzleからしばらくの間弾を発射する
    /// </summary>
    /// <param name="muzzle">発射するMuzzle</param>
    /// <param name="sec">発射秒数</param>
    /// <returns></returns>
    public IEnumerator TyottoShot(GameObject muzzle1, GameObject muzzle2, Transform pos, float sec = 0)
    {
        GameObject g1 = Instantiate(muzzle1, pos.position, Quaternion.identity);
        g1.transform.parent = this.transform;
        GameObject g2 = Instantiate(muzzle2, pos.position, Quaternion.identity);
        g2.transform.parent = this.transform;
        yield return new WaitForSeconds(sec);
        Destroy(g1);
        Destroy(g2);
    }

    /// <summary>
    /// Muzzleからしばらくの間弾を発射する
    /// </summary>
    /// <param name="muzzle">発射するMuzzle</param>
    /// <param name="sec">発射秒数</param>
    /// <returns></returns>
    public IEnumerator TyottoShot(GameObject muzzle1, GameObject muzzle2, GameObject muzzle3, Transform pos, float sec = 0)
    {
        GameObject g1 = Instantiate(muzzle1, pos.position, Quaternion.identity);
        g1.transform.parent = this.transform;
        GameObject g2 = Instantiate(muzzle2, pos.position, Quaternion.identity);
        g2.transform.parent = this.transform;
        GameObject g3 = Instantiate(muzzle3, pos.position, Quaternion.identity);
        g3.transform.parent = this.transform;
        yield return new WaitForSeconds(sec);
        Destroy(g1);
        Destroy(g2);
        Destroy(g3);
    }

    /// <summary>
    /// 二つの数値の割合(%)を返す
    /// </summary>
    /// <param name="now">現在の数値</param>
    /// <param name="max">最大の数値</param>
    /// <returns>二つの数値の割合</returns>
    public float Percent(float now, float max) { return (now / max) * 100; }

    /// <summary>
    /// 体力が一定量以下になったら形態移行する
    /// </summary>
    public virtual void FazeChanger() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet" || collision.tag == "Blast")
        {
            BulletBase bullet = collision.GetComponent<BulletBase>();
            m_life -= bullet.m_power;
            Vector2 v = new Vector2(this.transform.position.x + Random.Range(-1f, 1f), this.transform.position.y + Random.Range(-1f, 1f));
            var inst = Instantiate(m_damagePrefab, v, Quaternion.identity);
            inst.GetComponent<DamageText>().m_vec = v;
            Text text = inst.transform.GetChild(0).GetComponent<Text>();
            text.text = $"{bullet.m_power}";
            m_hpSlider.value = m_life;
        }

        if (m_life <= 0)
        {
            OnDead();
        }
        else
        {
            FazeChanger();
        }
    }

    private void OnDead()
    {
        if (m_rb.bodyType != RigidbodyType2D.Dynamic) { m_rb.bodyType = RigidbodyType2D.Dynamic; }
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        if (col) { col.enabled = false; }
        else { Debug.LogError("CircleCollider2D is null (Enemy.OnDead)"); }
        Vector2 v = new Vector2(Random.Range(-1f, 1f), 1);
        m_rb.AddForce(v * 3, ForceMode2D.Impulse);
        Invoke("Dead", 0.1f);
    }

    /// <summary>
    /// Invoke用
    /// </summary>
    private void Dead()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        transform.parent.gameObject.GetComponent<BossEriaCollider>().EndBoss();
    }

    public int Damage()
    {
        return m_power;
    }
}
