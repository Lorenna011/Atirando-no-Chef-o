using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("Configurações da bala")]
    public float speed = 10f;
    public float lifetime = 3f;
    public float damage = 10f;

    private PlayerBulletPool pool;
    private float timer;
    private bool returningToPool;

    public void Initialize(PlayerBulletPool bulletPool)
    {
        pool = bulletPool;
    }

    public void Launch()
    {
        timer = 0f;
        returningToPool = false;
    }

    private void Update()
    {
        if (returningToPool || !gameObject.activeInHierarchy)
            return;

        transform.position += transform.right * speed * Time.deltaTime;

        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            ReturnToPool();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (returningToPool)
            return;

        // Ignora a nave do jogador.
        if (other.CompareTag("Player"))
            return;

        // Procura o BossHealth no objeto atingido
        // ou nos objetos pais.
        BossHealth boss = other.GetComponentInParent<BossHealth>();

        if (boss == null)
        {
            Debug.Log("Colisão com " + other.name +
                      ", mas não encontrei BossHealth.");
            return;
        }

        Debug.Log("BALA ATINGIU O CHEFÃO!");

        boss.TakeDamage(damage);

        ReturnToPool();
    }
    
    private void ReturnToPool()
    {
        if (returningToPool)
            return;

        returningToPool = true;

        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}