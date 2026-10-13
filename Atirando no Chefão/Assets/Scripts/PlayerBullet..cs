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

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            pool.Release(this);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            pool.Release(this);
        }
    }
}