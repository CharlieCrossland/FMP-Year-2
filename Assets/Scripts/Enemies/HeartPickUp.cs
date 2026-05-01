using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.Instance.healthAmount += 1;
            HeartUI.Instance.DrawHearts();
            Destroy(this.gameObject);
        }
    }
}
