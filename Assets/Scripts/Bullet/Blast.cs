using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : NormalBullet
{
    [SerializeField] private GameObject m_damageEria;

    new public void OnTriggerEnter2D(Collider2D collision)
    {
        var g = Instantiate(m_damageEria, gameObject.transform.position, Quaternion.identity);
        Instantiate(m_effectPrefab, gameObject.transform.position, Quaternion.identity);
        g.GetComponent<bakuhatu>().m_power = m_power;
        Destroy(this.gameObject);
    }
}
