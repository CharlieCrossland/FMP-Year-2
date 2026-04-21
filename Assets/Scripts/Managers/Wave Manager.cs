using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public int numberOfEnemies;
    private int waveCount;
    private float waveEnemyMultiplier = 0.25f;
    private float maxNumberOfEnemies;

    bool waveSpawned;
    bool generateNewMax;

    [SerializeField] private Transform enemyParent;
    [SerializeField] private GameObject regularEnemyPrefab;

    private void Awake()
    {
        Instance = this;

        waveSpawned = false;
        waveCount = 1;
        maxNumberOfEnemies = 10;
    }

    private void Update()
    {
        WaveCheck();
    }

    private void WaveCheck()
    {
        // wave spawning
        if (!waveSpawned && GameStatesManager.Instance.currentState == GameStatesManager.GameStates.SpawnEnemies)
        {
            Debug.Log(GameStatesManager.Instance.currentState);

            for (numberOfEnemies = 0; numberOfEnemies < maxNumberOfEnemies; numberOfEnemies++)
            {
                SpawnEnemy();
            }

            StopEnemySpawn();
        }
        else if (waveSpawned && GameStatesManager.Instance.currentState == GameStatesManager.GameStates.SpawnEnemies)
        {
            generateNewMax = true;

            GameStatesManager.Instance.currentState = GameStatesManager.GameStates.Playing;
        }

        // wave counting and creating a new set amount of enemies to spawn
        if (GameStatesManager.Instance.currentState == GameStatesManager.GameStates.Playing)
        {
            CheckForAllEnemiesDead();
            GenerateMaxNumberOfEnemies();
        }
    }

    private void CheckForAllEnemiesDead()
    {
        if (numberOfEnemies <= 0)
        {
            waveCount++;

            if (waveCount % 5 == 0)
            {
                waveSpawned = false;
                GameStatesManager.Instance.currentState = GameStatesManager.GameStates.GracePeriod;
                return;
            }
            else
            {
                waveSpawned = false;
                GameStatesManager.Instance.currentState = GameStatesManager.GameStates.SpawnEnemies;
                return;
            }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(regularEnemyPrefab, enemyParent);
    }

    private void GenerateMaxNumberOfEnemies()
    {
        if (generateNewMax)
        {
            maxNumberOfEnemies += (waveCount * waveEnemyMultiplier);
            generateNewMax = false;
        }
    }

    private void StopEnemySpawn()
    {
        if (numberOfEnemies >= maxNumberOfEnemies)
        {
            waveSpawned = true;
        }
    }
}
