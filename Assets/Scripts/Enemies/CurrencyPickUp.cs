using UnityEngine;

public class CurrencyPickUp : MonoBehaviour
{
    private int value;

    private void Start()
    {
        value = Random.Range(5, 41);

        ChangeModel();
    }

    private void ChangeModel()
    {
        if (value >= 30)
        {
            Debug.Log("Change to gold");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GiveCurrency();
        }
    }

    void GiveCurrency()
    {
        SavedVariables.Instance.currentMoney += value;
        Destroy(this.gameObject);
    }
}
