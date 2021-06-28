using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum PtternState
{
    Normal,

}
public class BossTest : Enemy
{
    float _moveTimer;
    GameObject _player;
    Rigidbody2D _rb;
    Slider _slider;
    GameObject _canvas;
    
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
        HumanMove();
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
}
