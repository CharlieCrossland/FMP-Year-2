using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    float invincibilityTimer;
    [SerializeField] private float maxTime;
    bool playerHit;
    bool canTakeDamage;

    [Header("Player Health")]
    public int healthAmount;

    [SerializeField] private Slider slider;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        healthAmount = SavedVariables.Instance.maxHealth;
        canTakeDamage = true;
    }

    private void Update()
    {
        Invincibility();
        CheckForDeath();
        HealthBarUI();
    }

    private void CheckIfDamageCanBeTaken(GameObject otherObject)
    {
        if (canTakeDamage == true)
        {
            // take damage and set player hit as true
            TakeDamage();
            playerHit = true;
            Destroy(otherObject);
        }
        else
        {

        }
    }

    private void TakeDamage()
    {
        healthAmount -= 1;
    }

    private void CheckForDeath()
    {
        if (healthAmount <= 0)
        {
            GameStatesManager.Instance.currentState = GameStatesManager.GameStates.GameOver;
        }
    }

    private void Invincibility()
    {
        if (playerHit == true)
        {
            if (invincibilityTimer <= 0)
            {
                canTakeDamage = true;
                playerHit = false;
            }
            else
            {
                canTakeDamage = false;
                invincibilityTimer -= Time.deltaTime;
            }
        }
        else
        {
            invincibilityTimer = maxTime;
        }
    }

    void HealthBarUI()
    {
        slider.maxValue = SavedVariables.Instance.maxHealth;
        slider.value = healthAmount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            CheckIfDamageCanBeTaken(collision.gameObject);
        }
    }
}
