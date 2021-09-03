using UnityEngine;

public class NormalBullet : BulletBase
{
    private void Start()
    {
        SetState();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
