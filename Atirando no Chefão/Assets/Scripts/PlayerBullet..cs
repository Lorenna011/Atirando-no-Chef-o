using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    private PlayerBulletPool pool;
    private float timer;

    public void Initialize(PlayerBulletPool bulletPool)
    {
        pool = bulletPool;
    }

    public void Launch()
    {
        timer = 0f;
    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            if (pool != null)
                pool.Release(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            if (pool != null)
                pool.Release(this);
        }
    }
}