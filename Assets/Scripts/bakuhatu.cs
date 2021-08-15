using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bakuhatu : BulletBase
{
    private float m_destroyTime = 0.5f;
    private float m_timer = 0;

    void Update()
    {
        if (gameObject.transform.localScale.x < 10f)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + 0.5f, gameObject.transform.localScale.y + 0.5f);
        }
        m_timer += Time.deltaTime;
        if (m_timer > m_destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}
