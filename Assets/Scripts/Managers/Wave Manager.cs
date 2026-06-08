using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public int numberOfEnemiesSpawned;
    public int numberOfEnemiesInGame;
    public int waveCount;
    private float waveEnemyMultiplier = 2f;
    private float maxNumberOfEnemies;

    bool waveSpawned;
    bool generateNewMax;

    [SerializeField] private float effectFrequency;
    private float enemyHealthIncreaseFrequency = 5f;
    bool enemyHealthIncrease;

    [Header("Prefabs")]
    [SerializeField] private Transform enemyParent;
    private GameObject enemyPrefab;
    [SerializeField] private GameObject regularEnemyPrefab;
    [SerializeField] private GameObject bigEnemyPrefab;
    [SerializeField] private GameObject spearEnemyPrefab;
    [SerializeField] private TMP_Text waveCountText;

    [Header("Spawn Points")]
    [SerializeField] private Transform LeftSpawn;
    [SerializeField] private Transform TopSpawn;
    [SerializeField] private Transform RightSpawn;
    [SerializeField] private Transform BottomSpawn;
    private int randomSpawnNumber;
    private Transform spawnSelected;

    [Header("Spawn Timers")]
    [SerializeField] private float maxSpawnTimer;
    private float spawnTimer;

    private void Awake()
    {
        Instance = this;

        waveSpawned = false;
        waveCount = 0;
        maxNumberOfEnemies = 3;

        enemyHealthIncrease = false;
    }

    private void Update()
    {
        WaveCheck();
        WaveCountUI();
    }

    private void WaveCheck()
    {
        // wave spawning
        if (!waveSpawned && (GameStatesManager.Instance.currentState == GameStatesManager.GameStates.SpawnEnemies || GameStatesManager.Instance.currentState == GameStatesManager.GameStates.Playing))
        {
            if (waveCount % enemyHealthIncreaseFrequency == 0 && !enemyHealthIncrease)
            {
                SavedVariables.Instance.maxRegularHealth = SavedVariables.Instance.maxRegularHealth + 110;
                SavedVariables.Instance.maxBigHealth = SavedVariables.Instance.maxBigHealth + 100;
                enemyHealthIncrease = true;
            }

            if (spawnTimer <= 0)
            {
                //for (numberOfEnemies = 0; numberOfEnemies < maxNumberOfEnemies; numberOfEnemies++)
                //{
                //    SpawnEnemy();

                //    spawnTimer = maxSpawnTimer;
                //}

                SpawnEnemy();
            }
            else
            {
                spawnTimer -= Time.deltaTime;
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
        if (numberOfEnemiesInGame <= 0 && waveSpawned == true)
        {
            waveCount++;

            if (waveCount % effectFrequency == 0)
            {
                waveSpawned = false;
                numberOfEnemiesSpawned = 0;
                GameStatesManager.Instance.currentState = GameStatesManager.GameStates.GracePeriod;
                return;
            }
            else
            {
                waveSpawned = false;
                numberOfEnemiesSpawned = 0;
                GameStatesManager.Instance.currentState = GameStatesManager.GameStates.SpawnEnemies;
                return;
            }
        }
    }

    private void SpawnEnemy()
    {
        PickSpawn();
        PickEnemy();

        Instantiate(enemyPrefab, spawnSelected.transform.position, transform.rotation, enemyParent);

        numberOfEnemiesSpawned++;
        numberOfEnemiesInGame++;
        spawnTimer = maxSpawnTimer;
    }

    // pick random spawn for enemy to spawn at
    private void PickSpawn()
    {
        randomSpawnNumber = Random.Range(0, 4);

        switch (randomSpawnNumber)
        {
            case 0:
                spawnSelected = LeftSpawn;
                break;
            case 1:
                spawnSelected = TopSpawn;
                break;
            case 2:
                spawnSelected = RightSpawn;
                break;
            case 3:
                spawnSelected = BottomSpawn;
                break;
        }
    }

    private void PickEnemy()
    {
        float randomEnemyNumber = Random.Range(0, 11);

        switch (randomEnemyNumber)
        {
            case 0:
                enemyPrefab = regularEnemyPrefab;
                break;
            case 1:
                enemyPrefab = regularEnemyPrefab;
                break;
            case 2:
                enemyPrefab = bigEnemyPrefab;
                break;
            case 3:
                enemyPrefab = regularEnemyPrefab;
                break;
            case 4:
                enemyPrefab = bigEnemyPrefab;
                break;
            case 5:
                enemyPrefab = regularEnemyPrefab;
                break;
            case 6:
                enemyPrefab = regularEnemyPrefab;
                break;
            case 7:
                enemyPrefab = regularEnemyPrefab;
                break;
            case 8:
                enemyPrefab = regularEnemyPrefab;
                break;
            case 9:
                enemyPrefab = regularEnemyPrefab;
                break;
            case 10:
                enemyPrefab = regularEnemyPrefab;
                break;
        }    
    }

    private void GenerateMaxNumberOfEnemies()
    {
        if (generateNewMax)
        {
            maxNumberOfEnemies += (waveCount / waveEnemyMultiplier);
            generateNewMax = false;
        }
    }

    private void StopEnemySpawn()
    {
        if (numberOfEnemiesSpawned >= maxNumberOfEnemies)
        {
            waveSpawned = true;
        }
    }

    private void WaveCountUI()
    {
        waveCountText.SetText("Wave " + waveCount);
    }    
}
