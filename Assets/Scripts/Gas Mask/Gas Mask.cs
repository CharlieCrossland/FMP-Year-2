using UnityEngine;

public class GasMask : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChooseEffect.Instance.haveMask = true;
            Destroy(this.gameObject);
        }
    }
}
