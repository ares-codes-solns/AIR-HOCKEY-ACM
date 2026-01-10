using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    // 1 = Player 1 scores, 2 = Player 2 scores
    public int scoringPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Puck"))
            return;

        GameManager.Instance.GoalScored(scoringPlayer);
    }
}
