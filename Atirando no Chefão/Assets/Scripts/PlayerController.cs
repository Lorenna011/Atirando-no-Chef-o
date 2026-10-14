using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float speed = 5f;

    [Header("Disparo")]
    [SerializeField] private PlayerBulletPool bulletPool;
    [SerializeField] private Transform firePoint;
    [SerializeField, Range(0.2f, 1f)]
    private float fireInterval = 0.5f;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private Vector2 movement;
    private float nextFireTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

        if (rb == null)
            Debug.LogError("Player precisa de Rigidbody2D!");

        if (bulletPool == null)
            Debug.LogError("PlayerBulletPool não foi configurada!");

        if (firePoint == null)
            Debug.LogError("Ponto de fogo não foi configurado!");
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) &&
            Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireInterval;
        }
    }

    private void FixedUpdate()
    {
        if (rb == null)
            return;

        Vector2 newPosition = rb.position +
            movement.normalized * speed * Time.fixedDeltaTime;

        Camera cam = Camera.main;

        if (cam != null)
        {
            float distance = Mathf.Abs(
                cam.transform.position.z - transform.position.z
            );

            Vector3 bottomLeft =
                cam.ViewportToWorldPoint(new Vector3(0, 0, distance));

            Vector3 topRight =
                cam.ViewportToWorldPoint(new Vector3(1, 1, distance));

            float halfWidth = 0f;
            float halfHeight = 0f;

            if (playerCollider != null)
            {
                halfWidth = playerCollider.bounds.extents.x;
                halfHeight = playerCollider.bounds.extents.y;
            }

            newPosition.x = Mathf.Clamp(
                newPosition.x,
                bottomLeft.x + halfWidth,
                topRight.x - halfWidth
            );

            newPosition.y = Mathf.Clamp(
                newPosition.y,
                bottomLeft.y + halfHeight,
                topRight.y - halfHeight
            );
        }

        rb.MovePosition(newPosition);
    }

    private void Shoot()
    {
        if (bulletPool == null || firePoint == null)
            return;

        PlayerBullet bullet = bulletPool.GetBullet();

        if (bullet == null)
        {
            Debug.LogError("A Pool não retornou uma bala!");
            return;
        }

        bullet.transform.SetPositionAndRotation(
            firePoint.position,
            firePoint.rotation
        );

        bullet.gameObject.SetActive(true);
    }
}
