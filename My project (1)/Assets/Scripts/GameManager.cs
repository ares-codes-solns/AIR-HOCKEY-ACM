using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // -----------------------------
    // SINGLETON
    // -----------------------------
    public static GameManager Instance;

    // -----------------------------
    // GAME MODES
    // -----------------------------
    public enum GameMode
    {
        Timed,        // 1–2 minute match
        FirstToTen    // First to 10 goals wins
    }

    public GameMode currentMode = GameMode.FirstToTen;

    // -----------------------------
    // REFERENCES
    // -----------------------------
    public GameObject puck;
    Rigidbody2D puckRB;

    // -----------------------------
    // SCORES
    // -----------------------------
    public int scoreP1 = 0;
    public int scoreP2 = 0;

    // -----------------------------
    // TIMER (for Timed Mode)
    // -----------------------------
    public float matchTime = 60f;   // 60 or 120 seconds
    float currentTime;

    // -----------------------------
    // UI (OPTIONAL – safe if null)
    // -----------------------------
    public TMP_Text scoreTextP1;
    public TMP_Text scoreTextP2;
    public TMP_Text timerText;
    public TMP_Text resultText;

    // -----------------------------
    // GAME STATE
    // -----------------------------
    bool gameOver = false;

    // -----------------------------
    // UNITY METHODS
    // -----------------------------
    void Awake()
    {
        // Singleton setup
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        puckRB = puck.GetComponent<Rigidbody2D>();
        currentTime = matchTime;

        UpdateUI();

        if (resultText != null)
            resultText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameOver)
            return;

        if (currentMode == GameMode.Timed)
        {
            currentTime -= Time.deltaTime;

            if (timerText != null)
                timerText.text = Mathf.Ceil(currentTime).ToString();

            if (currentTime <= 0)
            {
                EndGame();
            }
        }
    }

    // -----------------------------
    // CALLED BY GOALS
    // -----------------------------
    public void GoalScored(int scoringPlayer)
    {
        if (gameOver)
            return;

        if (scoringPlayer == 1)
            scoreP1++;
        else if (scoringPlayer == 2)
            scoreP2++;

        UpdateUI();

        if (currentMode == GameMode.FirstToTen)
        {
            if (scoreP1 >= 10 || scoreP2 >= 10)
            {
                EndGame();
                return;
            }
        }

        StartCoroutine(ResetPuckDelayed());
    }

    // -----------------------------
    // PUCK RESET (SAFE)
    // -----------------------------
    IEnumerator ResetPuckDelayed()
    {
        puckRB.linearVelocity = Vector2.zero;
        puckRB.angularVelocity = 0f;

        yield return new WaitForSeconds(0.5f);

        puck.transform.position = Vector2.zero;
    }

    // -----------------------------
    // END GAME
    // -----------------------------
    void EndGame()
    {
        gameOver = true;

        puckRB.linearVelocity = Vector2.zero;
        puckRB.angularVelocity = 0f;

        string result;

        if (scoreP1 > scoreP2)
            result = "PLAYER 1 WINS!";
        else if (scoreP2 > scoreP1)
            result = "PLAYER 2 WINS!";
        else
            result = "DRAW!";

        if (resultText != null)
        {
            resultText.gameObject.SetActive(true);
            resultText.text = result;
        }

        Debug.Log(result);
    }

    // -----------------------------
    // UI UPDATE
    // -----------------------------
    void UpdateUI()
    {
        if (scoreTextP1 != null)
            scoreTextP1.text = scoreP1.ToString();

        if (scoreTextP2 != null)
            scoreTextP2.text = scoreP2.ToString();
    }

    // -----------------------------
    // RESTART GAME (OPTIONAL)
    // -----------------------------
    public void RestartGame()
    {
        scoreP1 = 0;
        scoreP2 = 0;
        currentTime = matchTime;
        gameOver = false;

        puckRB.linearVelocity = Vector2.zero;
        puckRB.angularVelocity = 0f;
        puck.transform.position = Vector2.zero;

        if (resultText != null)
            resultText.gameObject.SetActive(false);

        UpdateUI();
    }
}
