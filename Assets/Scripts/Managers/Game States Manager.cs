using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatesManager : MonoBehaviour
{
    public static GameStatesManager Instance;

    [Header("Start")]
    private float startTimer;
    [SerializeField] private TMP_Text startCountdownText;

    [Header("Game Over")]
    [SerializeField] private Animator deathAnimator;
    private WaitForSeconds gameOverAnimationTime = new(3.3f);

    public enum GameStates
    {
        Ready,
        Playing,
        SpawnEnemies,
        GracePeriod,
        Upgrade,
        GameOver,
    };

    public GameStates currentState;

    private void Awake()
    {
        Instance = this;

        currentState = GameStates.Ready;

        startTimer = 5f;
    }

    private void Update()
    {
        CheckGameState();
    }

    private void CheckGameState()
    {
        if (currentState == GameStates.Ready)
        {
            if (startTimer <= 0)
            {
                startCountdownText.enabled = false;
                currentState = GameStates.Playing;
            }
            else
            {
                startTimer -= Time.deltaTime;

                int timerInt = (int)startTimer + 1;
                startCountdownText.enabled = true;
                startCountdownText.SetText(timerInt.ToString());
            }
        }
        if (currentState == GameStates.GameOver)
        {
            StartCoroutine(GameOverState());
        }
    }

    private IEnumerator GameOverState()
    {
        deathAnimator.SetTrigger("Death");
        yield return gameOverAnimationTime;
        SceneManager.LoadScene("LeaderboardInput");
        yield break;
    }
}
