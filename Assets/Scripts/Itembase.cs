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
    [SerializeField] private StatusItems m_statusItems = StatusItems.Life;
    [SerializeField] private GameObject m_textPrefabs;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (m_itemType == ItemType.Wepon)
            {
                player.m_haveBullet++;
            }

            if (m_itemType == ItemType.Status)
            {
                player.GetStatusItem(m_statusItems);
            }
            Instantiate(m_textPrefabs, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
