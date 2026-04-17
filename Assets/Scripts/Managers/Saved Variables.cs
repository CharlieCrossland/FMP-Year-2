using UnityEngine;

public class SavedVariables : MonoBehaviour
{
    public static SavedVariables Instance;

    public float bulletDamage {  get; private set; }
    public float currentScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        bulletDamage = 50f;
    }
}
