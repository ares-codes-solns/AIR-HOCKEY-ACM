using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int scoreP1 = 0;
    public int scoreP2 = 0;

    public GameObject puck;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GoalScored(int player)
    {
        if (player == 1)
            scoreP1++;
        else if (player == 2)
            scoreP2++;

        Debug.Log("Score → P1: " + scoreP1 + " | P2: " + scoreP2);

        ResetPuck();
    }

    void ResetPuck()
    {
        Rigidbody2D rb = puck.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        puck.transform.position = Vector2.zero;
    }
}
