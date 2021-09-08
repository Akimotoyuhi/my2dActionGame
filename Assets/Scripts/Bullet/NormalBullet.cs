using UnityEngine;

public class NormalBullet : BulletBase
{
    private void Start()
    {
        SetState();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(m_effectPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
