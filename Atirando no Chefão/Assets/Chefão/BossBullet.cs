using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 10f;
    public float lifetime = 5f;

    private BossBulletPool pool;
    private Vector2 direction;
    private float timer;

    public void Initialize(BossBulletPool bulletPool)
    {
        pool = bulletPool;
    }

    public void Launch(Vector2 newDirection)
    {
        direction = newDirection.normalized;
        timer = 0f;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy)
            return;

        transform.position +=
            (Vector3)(direction * speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= lifetime)
            ReturnToPool();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aqui poderá ser chamada a vida do jogador.
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        if (pool != null)
            pool.Release(this);
        else
            gameObject.SetActive(false);
    }
}