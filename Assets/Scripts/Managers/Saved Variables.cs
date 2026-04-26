using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedVariables : MonoBehaviour
{
    public static SavedVariables Instance;

    public float bulletDamage;
    public float currentScore;

    bool resetScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game") && !resetScore)
        {
            currentScore = 0;
            resetScore = true;
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu") && resetScore)
        {
            resetScore = false;
        }
    }
}
