using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + movement.normalized * speed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -7.5f, 7.5f);
        newPosition.y = Mathf.Clamp(newPosition.y, -4f, 4f);

        rb.MovePosition(newPosition);
    }
}