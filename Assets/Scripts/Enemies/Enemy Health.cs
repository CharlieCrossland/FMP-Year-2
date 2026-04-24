using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class EnemyHealth : MonoBehaviour
{
    float currentHealth;
    [SerializeField] private float maxHealth;
    bool scoreSent;

    private Color red = new(1, 0, 0, 1);
    private Color white = new(0, 0, 0, 1);

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        sr.color = white;

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

            StartCoroutine(ChangeColour());

            Destroy(other.gameObject);
        }
    }

    IEnumerator ChangeColour()
    {
        sr.color = red;
        yield return new WaitForSeconds(0.3f);
        sr.color = white;
        yield break;
    }

    public void OnDestroy()
    {
        WaveManager.Instance.numberOfEnemies--;
        PlayerMovement.Instance.dashBarAmount++;
    }
}
