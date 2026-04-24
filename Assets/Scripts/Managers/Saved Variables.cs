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

        //GameObject[] objs = GameObject.FindGameObjectsWithTag("Saved Variables");

        //if (objs.Length > 1)
        //{
        //    Destroy(this.gameObject);
        //}

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
