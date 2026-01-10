using UnityEngine;

public class PaddleController_P2 : MonoBehaviour
{
    public float speed = 15f;

    Rigidbody2D rb;
    Vector2 targetPos;

    void Awake()
    {
        Debug.Log("PaddleController_P2 running on: " + gameObject.name);

        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError(
                "NO Rigidbody2D on object: " + gameObject.name
            );
            enabled = false;
            return;
        }

        targetPos = rb.position;
    }

    void Update()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.A)) x = -1f;
        if (Input.GetKey(KeyCode.D)) x = 1f;
        if (Input.GetKey(KeyCode.W)) y = 1f;
        if (Input.GetKey(KeyCode.S)) y = -1f;

        targetPos += new Vector2(x, y) * speed * Time.deltaTime;

        // Clamp to TOP half
        targetPos.x = Mathf.Clamp(targetPos.x, -7.5f, 7.5f);
        targetPos.y = Mathf.Clamp(targetPos.y, 0f, 4.5f);
    }

    void FixedUpdate()
    {
        rb.MovePosition(targetPos);
    }
}
