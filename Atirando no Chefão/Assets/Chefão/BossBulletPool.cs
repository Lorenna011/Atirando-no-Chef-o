using System.Collections.Generic;
using UnityEngine;

public class BossBulletPool : MonoBehaviour
{
    public BossBullet bulletPrefab;
    public int quantidadeInicial = 20;

    private Queue<BossBullet> pool =
        new Queue<BossBullet>();

    private void Awake()
    {
        for (int i = 0; i < quantidadeInicial; i++)
        {
            CriarBala();
        }
    }

    private BossBullet CriarBala()
    {
        BossBullet bala = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );

        bala.Initialize(this);
        bala.gameObject.SetActive(false);
        pool.Enqueue(bala);

        return bala;
    }

    public BossBullet GetBullet()
    {
        if (pool.Count == 0)
            CriarBala();

        BossBullet bala = pool.Dequeue();
        bala.gameObject.SetActive(true);

        return bala;
    }

    public void Release(BossBullet bala)
    {
        if (bala == null)
            return;

        bala.gameObject.SetActive(false);
        bala.transform.SetParent(transform);
        pool.Enqueue(bala);
    }
}