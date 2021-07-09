using UnityEngine;

public class NormalBullet : BulletBase
{
    //private Rigidbody2D m_rb;
    private void Start()
    {
        InitialVelocity();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
