using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [Header("Costs")]
    private int damageCost = 50;
    private int healthCost = 50;
    private int speedCost = 50;

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

        currentDamage.SetText(SavedVariables.Instance.bulletDamage.ToString());
        currentHealth.SetText(SavedVariables.Instance.maxHealth.ToString());
        currentSpeed.SetText(SavedVariables.Instance.speed.ToString());
    }

    private void DisplayNewValues()
    {
        float x = SavedVariables.Instance.bulletDamage + (SavedVariables.Instance.bulletDamage / 5);
        float y = SavedVariables.Instance.maxHealth + 1;
        float z = SavedVariables.Instance.speed + (SavedVariables.Instance.speed / 15);

        newDamage.SetText(x.ToString());
        newHealth.SetText(y.ToString());
        newSpeed.SetText(z.ToString());
    }

    #region BuyButtons

    public void DamageBuy()
    {
        if (SavedVariables.Instance.currentMoney >= damageCost)
        {
            SavedVariables.Instance.bulletDamage += (SavedVariables.Instance.bulletDamage / 5);

            SavedVariables.Instance.currentMoney -= damageCost;

            damageCost += (damageCost / 10);
        }
    }

    public void HealthBuy()
    {
        if (SavedVariables.Instance.currentMoney >= healthCost)
        {
            SavedVariables.Instance.maxHealth += 1;

            SavedVariables.Instance.currentMoney -= healthCost;

            healthCost += (healthCost / 10);
        }
    }

    public void SpeedBuy()
    {
        if (SavedVariables.Instance.currentMoney >= speedCost)
        {
            SavedVariables.Instance.speed += (SavedVariables.Instance.speed / 15);

            SavedVariables.Instance.currentMoney -= speedCost;

            speedCost += (speedCost / 10);
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
