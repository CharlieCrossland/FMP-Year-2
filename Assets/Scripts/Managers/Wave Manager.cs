using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public int numberOfEnemies;
    private int waveCount;
    private float waveEnemyMultiplier = 0.5f;
    private float maxNumberOfEnemies;

    bool waveSpawned;
    bool generateNewMax;

    [SerializeField] private Transform enemyParent;
    [SerializeField] private GameObject regularEnemyPrefab;
    [SerializeField] private TMP_Text waveCountText;

    [Header("Spawn Points")]
    private int randomSpawnNumber;
    private Transform spawnSelected;
    [SerializeField] private Transform LeftSpawn;
    [SerializeField] private Transform TopSpawn;
    [SerializeField] private Transform RightSpawn;
    [SerializeField] private Transform BottomSpawn;

    private void Awake()
    {
        Instance = this;

        waveSpawned = false;
        waveCount = 0;
        maxNumberOfEnemies = 10;
    }

    private void Update()
    {
        WaveCheck();
        WaveCountUI();
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
        PickSpawn();

        Instantiate(regularEnemyPrefab, spawnSelected.transform.position, transform.rotation, enemyParent);
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
        if (numberOfEnemies >= maxNumberOfEnemies)
        {
            waveSpawned = true;
        }
    }

    private void WaveCountUI()
    {
        waveCountText.SetText("Wave " + waveCount);
    }    
}
