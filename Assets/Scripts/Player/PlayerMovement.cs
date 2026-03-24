using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 currentMovement;
    public float moveSpeed;

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        currentMovement = PlayerInputHandler.Instance.MovementInput.normalized * Time.deltaTime * moveSpeed;

        transform.Translate(currentMovement);
    }

    void HandleRotation()
    {
        
    }
}
