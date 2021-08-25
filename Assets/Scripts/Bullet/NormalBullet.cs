using UnityEngine;

public class NormalBullet : BulletBase
{
    private void Start()
    {
        //Instantiate(m_particle, transform.position, Quaternion.identity);
        InitialVelocity();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
