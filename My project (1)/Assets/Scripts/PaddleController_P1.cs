using UnityEngine;

public class PaddleController_P1 : MonoBehaviour
{
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
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) x = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) x = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) y = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) y = -1f;

        targetPos += new Vector2(x, y) * speed * Time.deltaTime;

        // Clamp to bottom half
        targetPos.x = Mathf.Clamp(targetPos.x, -7.5f, 7.5f);
        targetPos.y = Mathf.Clamp(targetPos.y, -4.5f, 0f);
    }

    void FixedUpdate()
    {
        rb.MovePosition(targetPos);
    }
}
