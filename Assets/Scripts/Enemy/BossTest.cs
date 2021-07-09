using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum PatternState
{
    A,
    B,
    C,
    D
}

public class BossTest : Enemy
{
    private float _moveTimer;
    private GameObject _player;
    private Rigidbody2D _rb;
    private Slider _slider;
    private GameObject _canvas;
    [SerializeField] private GameObject _muzzle1;
    [SerializeField] private GameObject _muzzle2;
    [SerializeField] private GameObject _muzzle3;
    [SerializeField] private GameObject _muzzle4;
    [SerializeField] private Transform _anchorA;
    [SerializeField] private Transform _anchorB;
    public PatternState _patternState = PatternState.A;
    private bool _isBullet = false;
    private bool _nowTransform = false;

    void Start()
    {
        _player = GameObject.Find("Player");
        _rb = GetComponent<Rigidbody2D>();
        _canvas = GameObject.Find("Canvas");
        _slider = _canvas.transform.Find("EnemyHPgage").GetComponent<Slider>();
        _slider.maxValue = m_maxLife;
    }


    void Update()
    {
        _slider.value = m_life;
        AtPlayer();
        PatternStateChanged();
        if (_patternState == PatternState.A)
        {
            HumanMove();
            _muzzle1.SetActive(true);
            _muzzle2.SetActive(false);
            _muzzle3.SetActive(false);
            _muzzle4.SetActive(false);
        }
        if (_patternState == PatternState.B)
        {
            if (!_isBullet) { StartCoroutine("PatternB"); }
            _muzzle1.SetActive(false);
            _muzzle2.SetActive(true);
            _muzzle3.SetActive(false);
            _muzzle4.SetActive(false);
        }
        if (_patternState == PatternState.C)
        {
            _muzzle1.SetActive(false);
            _muzzle2.SetActive(false);
            _muzzle3.SetActive(true);
            _muzzle4.SetActive(false);
        }
        if (_patternState == PatternState.D)
        {
            _muzzle1.SetActive(false);
            _muzzle2.SetActive(false);
            _muzzle3.SetActive(false);
            _muzzle4.SetActive(true);
        }
    }

    public void HumanMove()
    {
        if (_player)
        {
            _moveTimer += Time.deltaTime;
            if (_moveTimer > m_moveinterval)
            {
                if (_player.transform.position.x < this.transform.position.x)
                {
                    for (float i = 0; i < 3; i += Time.deltaTime)
                    {
                        _rb.velocity = new Vector2(-m_speed, _rb.velocity.y);
                    }
                }
                else
                {
                    for (float i = 0; i < 3; i += Time.deltaTime)
                    {
                        _rb.velocity = new Vector2(m_speed, _rb.velocity.y);
                    }
                }
                _moveTimer = 0;
            }
        }
    }

    IEnumerator PatternB()
    {
        _isBullet = true;
        if (_nowTransform) 
        { 
            transform.position = new Vector2(_anchorB.position.x, _anchorB.position.y);
            _nowTransform = false;
        }
        else
        {
            transform.position = new Vector2(_anchorA.position.x, _anchorA.position.y);
            _nowTransform = true;
        }
        yield return new WaitForSeconds(m_moveinterval);
        _isBullet = false;
    }

    private void PatternStateChanged()
    {
        if (75 < Percent(m_life, m_maxLife))
        {
            _patternState = PatternState.A;
        }
        else if (50 < Percent(m_life, m_maxLife))
        {
            _patternState = PatternState.B;
        }
        else if (25 < Percent(m_life, m_maxLife))
        {
            _patternState = PatternState.C;
        }
        else
        {
            _patternState = PatternState.D;
        }
    }

    private float Percent(float now, float max) { return (now / max) * 100; }
}
