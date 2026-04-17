using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class EnemyHealth : MonoBehaviour
{
    float currentHealth;
    [SerializeField] private float maxHealth;
    bool scoreSent;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        scoreSent = false;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        CheckHealth();
    }

    void CheckHealth()
    {
        // use object pooling when available
        if (currentHealth <= 0)
        {
            if (!scoreSent)
            {
                SavedVariables.Instance.currentScore += 10f;
                scoreSent = true;
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            currentHealth -= SavedVariables.Instance.bulletDamage;
        }
    }
}
