using UnityEngine;

public class NormalBullet : BulletBase
{
    [SerializeField] protected GameObject m_effectPrefab;
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
