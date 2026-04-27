using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedVariables : MonoBehaviour
{
    public static SavedVariables Instance;

    [Header("Player Attributes")]
    public float bulletDamage;
    public int maxHealth;
    public float speed;

    public float currentScore;
    public float currentMoney;

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

        bulletDamage = 12;
        maxHealth = 2;
        speed = 6;
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
