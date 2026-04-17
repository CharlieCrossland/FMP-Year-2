using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatesManager : MonoBehaviour
{
    public static GameStatesManager Instance;

    [Header("Game Over")]
    [SerializeField] private Animator deathAnimator;
    private WaitForSeconds gameOverAnimationTime = new(3.3f);

    public enum GameStates
    {
        Ready,
        Playing,
        GracePeriod,
        GameOver,
    };

    public GameStates currentState;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        CheckGameState();
    }

    private void CheckGameState()
    {
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
