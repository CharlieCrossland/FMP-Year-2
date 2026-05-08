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

    [Header("Enemy Attributes")]
    public float maxRegularHealth;
    public float maxBigHealth;

    bool resetVariables;

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
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game") && !resetVariables)
        {
            currentScore = 0;
            currentMoney = 0;
            bulletDamage = 12;
            maxHealth = 2;
            speed = 6;
            resetVariables = true;
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu") && resetVariables)
        {
            resetVariables = false;
        }
    }
}
