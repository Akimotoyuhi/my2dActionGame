using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageText : MonoBehaviour
{
    void Start()
    {
        Vector2 v = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        float power = Random.Range(1f, 5f);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(v * power, ForceMode2D.Impulse);
    }
}
