using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float invincibilityTimer;
    [SerializeField] private float maxTime;
    bool playerHit;
    bool canTakeDamage;

    [Header("Player Health")]
    private int healthAmount;
    [SerializeField] private int maxHealthAmount;

    private void Awake()
    {
        healthAmount = maxHealthAmount;
        canTakeDamage = true;
    }

    private void Update()
    {
        Invincibility();
        CheckForDeath();
    }

    private void CheckIfDamageCanBeTaken()
    {
        if (canTakeDamage == true)
        {
            // take damage and set player hit as true
            TakeDamage();
            playerHit = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            CheckIfDamageCanBeTaken();
        }
    }
}
