using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float y;
    private float timer = 0;
    void Start()
    {
        
    }

    void Update()
    {
        y += 0.01f;
        transform.position = new Vector2(transform.position.x, y);

        timer += Time.deltaTime;
        if (timer > 1)
        {
            Destroy(this.gameObject);
        }
    }
}
