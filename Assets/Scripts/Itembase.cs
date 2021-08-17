using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itembase : MonoBehaviour
{
    enum ItemType
    {
        Wepon,
        Status
    }
    [SerializeField] private ItemType m_itemType = ItemType.Status;
    //[SerializeField] private Wepon m_wepon = Wepon.Blast;
    [SerializeField] private StatusItems m_statusItems = StatusItems.Life;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (m_itemType == ItemType.Wepon)
            {
                player.m_haveBullet++;
            }
            else if (m_itemType == ItemType.Status)
            {
                player.m_haveItem[(int)m_statusItems]++;
            }

            Destroy(this.gameObject);
        }
    }
}
