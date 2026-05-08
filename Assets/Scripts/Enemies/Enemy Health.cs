using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    bool scoreSent;
    [SerializeField] private int scoreToSend;

    private Color red = new(1, 0, 0, 1);
    private Color white = new(0, 0, 0, 1);

    public Transform currencyPrefab;
    public Transform heartPrefab;

    private SpriteRenderer sr;
    private Animator animator;

    public bool isRegularEnemy;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        sr.color = white;

        scoreSent = false;

        if (isRegularEnemy)
        {
            currentHealth = SavedVariables.Instance.maxRegularHealth;
        }
        else
        {
            currentHealth = SavedVariables.Instance.maxBigHealth;
        }
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
                SavedVariables.Instance.currentScore += scoreToSend;
                scoreSent = true;
            }

            StartCoroutine(DeathAnimation());
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

    public void ItemDrop()
    {
        float x = Random.Range(0, 11);

        if (x == 7 && PlayerHealth.Instance.healthAmount != SavedVariables.Instance.maxHealth)
        {
            Instantiate(heartPrefab, this.transform.position, this.transform.rotation);
        }
        else
        {
            Instantiate(currencyPrefab, this.transform.position, this.transform.rotation);
        }
    }

    IEnumerator DeathAnimation()
    {
        animator.SetTrigger("death");
        yield return new WaitForSeconds(0.65f);
        ItemDrop();
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        WaveManager.Instance.numberOfEnemies--;
        PlayerMovement.Instance.dashBarAmount++;
    }
}
