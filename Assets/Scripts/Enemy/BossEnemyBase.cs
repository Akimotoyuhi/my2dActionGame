using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBase : MonoBehaviour
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
    [System.NonSerialized] public GameObject m_player;
    [System.NonSerialized] public Rigidbody2D m_rb;
    [System.NonSerialized] public Animator m_anim;

    public delegate IEnumerator Actions();

    public void SetUp()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.FindWithTag("Player");
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
    /// <param name="sec">発射秒数</param>
    /// <returns></returns>
    public IEnumerator AllShot(ShotFoword[] muzzle, float sec)
    {
        for (int i = 0; i < muzzle.Length; i++)
        {
            muzzle[i].StopDisable();
        }
        yield return new WaitForSeconds(sec);
        for (int i = 0; i < muzzle.Length; i++)
        {
            muzzle[i].StopEnable();
        }
    }

    /// <summary>
    /// Muzzleからしばらくの間弾を発射する
    /// </summary>
    /// <param name="muzzle">発射するMuzzle</param>
    /// <param name="sec">発射秒数</param>
    /// <returns></returns>
    public IEnumerator TyottoShot(ShotFoword muzzle, float sec)
    {
        muzzle.StopDisable();
        yield return new WaitForSeconds(sec);
        muzzle.StopEnable();
    }

    /// <summary>
    /// Muzzleからしばらくの間弾を発射する
    /// </summary>
    /// <param name="muzzle">発射するMuzzle</param>
    /// <param name="sec">発射秒数</param>
    /// <returns></returns>
    public IEnumerator TyottoShot(ShotFoword muzzle1, ShotFoword muzzle2, float sec)
    {
        muzzle1.StopDisable();
        muzzle2.StopDisable();
        yield return new WaitForSeconds(sec);
        muzzle1.StopEnable();
        muzzle2.StopEnable();
    }

    /// <summary>
    /// Muzzleからしばらくの間弾を発射する
    /// </summary>
    /// <param name="muzzle">発射するMuzzle</param>
    /// <param name="sec">発射秒数</param>
    /// <returns></returns>
    public IEnumerator TyottoShot(ShotFoword muzzle1, ShotFoword muzzle2, ShotFoword muzzle3, float sec)
    {
        muzzle1.StopDisable();
        muzzle2.StopDisable();
        muzzle2.StopDisable();
        yield return new WaitForSeconds(sec);
        muzzle1.StopEnable();
        muzzle2.StopEnable();
        muzzle3.StopEnable();
    }

    public void AllEnable(ShotFoword[] shot)
    {
        for (int i = 0; i < shot.Length; i++)
        {
            shot[i].StopEnable();
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
