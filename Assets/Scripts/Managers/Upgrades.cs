using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [Header("Costs")]
    private int damageCost = 35;
    private int healthCost = 50;
    private int speedCost = 20;

    bool upgradeMenuDropped;

    [Header("References")]
    [SerializeField] private TMP_Text currencyAmountText;
    [SerializeField] private Animator animator;

    [Header("Cost Text")]
    [SerializeField] private TMP_Text damageCostText;
    [SerializeField] private TMP_Text healthCostText;
    [SerializeField] private TMP_Text speedCostText;

    [Header("Current Attribute Text")]
    [SerializeField] private TMP_Text currentDamage;
    [SerializeField] private TMP_Text currentHealth;
    [SerializeField] private TMP_Text currentSpeed;

    [Header("New Attribute Text")]
    [SerializeField] private TMP_Text newDamage;
    [SerializeField] private TMP_Text newHealth;
    [SerializeField] private TMP_Text newSpeed;

    private void Update()
    {
        CheckGameState();
        DisplayCost();
        DisplayCurrentValues();
        DisplayNewValues();
    }

    void CheckGameState()
    {
        if (GameStatesManager.Instance.currentState == GameStatesManager.GameStates.Upgrade)
        {
            BringUpgradeMenu();
        }
        else
        {

        }
    }

    private void BringUpgradeMenu()
    {
        if (!upgradeMenuDropped)
        {
            animator.SetTrigger("drop");
            upgradeMenuDropped = true;
        }
    }

    private void DisplayCost()
    {
        damageCostText.SetText("Cost: " + damageCost);
        healthCostText.SetText("Cost: " + healthCost);
        speedCostText.SetText("Cost: " + speedCost);
    }

    private void DisplayCurrentValues()
    {
        currencyAmountText.SetText("Currency: " + SavedVariables.Instance.currentMoney);

        currentDamage.SetText(SavedVariables.Instance.bulletDamage.ToString("F1"));
        currentHealth.SetText(SavedVariables.Instance.maxHealth.ToString("F1"));
        currentSpeed.SetText(SavedVariables.Instance.speed.ToString("F1"));
    }

    private void DisplayNewValues()
    {
        float x = SavedVariables.Instance.bulletDamage + (10 / (SavedVariables.Instance.bulletDamage / 4));
        float y = SavedVariables.Instance.maxHealth + 1;
        float z = SavedVariables.Instance.speed + 0.5f;

        newDamage.SetText(x.ToString("F1"));
        newHealth.SetText(y.ToString("F1"));
        newSpeed.SetText(z.ToString("F1"));
    }

    #region BuyButtons

    public void DamageBuy()
    {
        if (SavedVariables.Instance.currentMoney >= damageCost)
        {
            SavedVariables.Instance.bulletDamage += 10 / (SavedVariables.Instance.bulletDamage / 4);

            SavedVariables.Instance.currentMoney -= damageCost;

            damageCost += (damageCost / 3);
        }
    }

    public void HealthBuy()
    {
        if (SavedVariables.Instance.currentMoney >= healthCost)
        {
            SavedVariables.Instance.maxHealth += 1;

            SavedVariables.Instance.currentMoney -= healthCost;

            HeartUI.Instance.DrawHearts();

            healthCost += (healthCost / 2);
        }
    }

    public void SpeedBuy()
    {
        if (SavedVariables.Instance.currentMoney >= speedCost)
        {
            SavedVariables.Instance.speed += 0.5f;

            SavedVariables.Instance.currentMoney -= speedCost;

            speedCost += (speedCost / 2);
        }
    }

    #endregion

    public void Done()
    {
        animator.SetTrigger("return");
        GameStatesManager.Instance.currentState = GameStatesManager.GameStates.SpawnEnemies;
        upgradeMenuDropped = false;
    }
}
