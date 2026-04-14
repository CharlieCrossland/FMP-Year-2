using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int numberOfEnemies;
    private int waveCount;
    private float waveEnemyMultiplier = 0.25f;
    private float maxNumberOfEnemies;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        GenerateMaxNumberOfEnemies();
    }

    private void GenerateMaxNumberOfEnemies()
    {
        maxNumberOfEnemies = waveCount * waveEnemyMultiplier;
    }

    private void PreventEnemyOverflow()
    {
        if (numberOfEnemies >= (int)maxNumberOfEnemies)
        {

        }
    }
}
