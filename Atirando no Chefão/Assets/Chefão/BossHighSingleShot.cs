using UnityEngine;

public class BossHighSingleShot : MonoBehaviour
{
    public BossBulletPool bulletPool;
    public Transform firePoint;
    public Transform player;

    public float intervaloTiros = 2f;
    public float velocidadeBala = 5f;

    private float cronometro;
    private bool podeAtirar = false;

    private void Update()
    {
        if (!podeAtirar)
            return;

        if (player == null || firePoint == null ||
            bulletPool == null)
            return;

        cronometro += Time.deltaTime;

        if (cronometro >= intervaloTiros)
        {
            Atirar();
            cronometro = 0f;
        }
    }

    private void Atirar()
    {
        BossBullet bala = bulletPool.GetBullet();

        bala.transform.position = firePoint.position;
        bala.speed = velocidadeBala;

        Vector2 direcao =
            (player.position - firePoint.position).normalized;

        bala.Launch(direcao);
    }

    public void AtivarComportamento()
    {
        cronometro = 0f;
        podeAtirar = true;
    }

    public void DesativarComportamento()
    {
        podeAtirar = false;
    }
}