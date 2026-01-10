using UnityEngine;

public class MousePaddleController : MonoBehaviour
{
    public bool isRightPlayer; // false = LEFT player, true = RIGHT player
    public float speed = 15f;

    Rigidbody2D rb;
    Vector2 targetPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = rb.position;
    }

    void Update()
    {
        // Only move while mouse is HELD
        if (!Input.GetMouseButton(0))
            return;

        Vector2 mouseWorld =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // LEFT player
        if (!isRightPlayer && mouseWorld.x < 0)
            targetPos = mouseWorld;

        // RIGHT player
        if (isRightPlayer && mouseWorld.x > 0)
            targetPos = mouseWorld;

        // Clamp LEFT / RIGHT halves
        if (isRightPlayer)
            targetPos.x = Mathf.Clamp(targetPos.x, 0.5f, 7.5f);
        else
            targetPos.x = Mathf.Clamp(targetPos.x, -7.5f, -0.5f);

        // Both players share same vertical range
        targetPos.y = Mathf.Clamp(targetPos.y, -4.5f, 4.5f);
    }

    void FixedUpdate()
    {
        rb.MovePosition(
            Vector2.Lerp(rb.position, targetPos,
            speed * Time.fixedDeltaTime)
        );
    }
}

