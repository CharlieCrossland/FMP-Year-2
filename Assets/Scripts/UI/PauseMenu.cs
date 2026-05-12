using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    bool gamePaused;

    private void Start()
    {
        pauseMenu.SetActive(false);
        gamePaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == false) // turn on pause menu
        {
            Debug.Log("Pause Game");

            pauseMenu.SetActive(true);
            gamePaused = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == true) // turn off pause menu
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            gamePaused = false;
        }
    }

    public void BackToGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LeaderboardInput");
    }
}
