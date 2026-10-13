using UnityEngine;
using UnityEngine.Pool;

public class PlayerBulletPool : MonoBehaviour
{
    public PlayerBullet bulletPrefab;
    public int initialSize = 20;
    public int maxSize = 50;

    private ObjectPool<PlayerBullet> pool;

    void Awake()
    {
        pool = new ObjectPool<PlayerBullet>(
            CreateBullet,
            OnTakeFromPool,
            OnReturnToPool,
            OnDestroyBullet,
            false,
            initialSize,
            maxSize
        );
    }

    PlayerBullet CreateBullet()
    {
        PlayerBullet bullet = Instantiate(bulletPrefab);
        bullet.Initialize(this);
        bullet.gameObject.SetActive(false);
        return bullet;
    }

    void OnTakeFromPool(PlayerBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    void OnReturnToPool(PlayerBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    void OnDestroyBullet(PlayerBullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public PlayerBullet GetBullet()
    {
        PlayerBullet bullet = pool.Get();
        bullet.Launch();
        return bullet;
    }

    public void Release(PlayerBullet bullet)
    {
        if (bullet != null && bullet.gameObject.activeSelf)
        {
            pool.Release(bullet);
        }
    }
}