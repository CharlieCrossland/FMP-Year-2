using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 currentMovement;
    public Rigidbody2D rb;
    public float moveSpeed;
    public float dashSlowTimeScale;

    private float slowTimer;
    public float maxSlowTimer;
    private bool dashSlowActive;
    private bool hasSlowBeenUsed;
    private bool canDash;
    public float maxDashDistance;
    public GameObject dashOutline;

    private Vector3 mousePosition;

    bool isFacingRight;

    private void Awake()
    {
        slowTimer = maxSlowTimer;
        isFacingRight = true;

        Cursor.lockState = CursorLockMode.Confined;

        debugDashDestination.SetActive(false);
    }

    private void Update()
    {
        MovementDirection();
        DashOutline();
    }

    void FixedUpdate()
    {
        DashHandler();
        HandleMovement();
    }

    void HandleMovement()
    {
        currentMovement = PlayerInputHandler.Instance.MovementInput * Time.fixedDeltaTime * moveSpeed;

        //rb.position += (Vector2)currentMovement;
        rb.MovePosition(rb.position + (Vector2)currentMovement);
    }

    void MovementDirection()
    {
        float inputs = PlayerInputHandler.Instance.MovementInput.x;

        if (isFacingRight && inputs < -.1f)
        {
            Flip();
        }
        else if (!isFacingRight && inputs > .1f)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    #region Dashing

    public GameObject debugDashDestination;
    void DashHandler()
    {
        if (PlayerInputHandler.Instance.DashTriggered)
        {
            if (hasSlowBeenUsed == false)
            {
                dashSlowActive = true;
            }
            dashOutline.SetActive(true);

            canDash = true;
        }
        else
        {
            CheckDash();

            dashOutline.SetActive(false);

            canDash = false;
        }

        if (dashSlowActive == true)
        {
            DashSlowTime();
        }
        else
        {
            ResetTimeScale();
        }    
    }

    void DashOutline()
    {
        // setting an outline of where the player will dash to, disappears when the player lets go
        if (canDash)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            dashOutline.transform.position = new(mousePosition.x, mousePosition.y);
        }
    }

    void DashSlowTime()
    {
        if (slowTimer <= 0)
        {
            ResetTimeScale();
            dashSlowActive = false;
            hasSlowBeenUsed = true;
            slowTimer = maxSlowTimer;
        }
        else if (dashSlowActive)
        {
            slowTimer -= Time.unscaledDeltaTime;

            // slow time down
            Time.timeScale = dashSlowTimeScale;
        }
    }

    void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

    void CheckDash()
    {
        if (canDash)
        {
            if (PlayerInputHandler.Instance.DashTriggered == false)
            {
                dashSlowActive = false;
                slowTimer = maxSlowTimer;
                hasSlowBeenUsed = false;

                TriggerDash();
                Debug.Log("Player Dash");
            }
        }
    }

    void TriggerDash()
    {
#if UNITY_EDITOR
        debugDashDestination.SetActive(true);
        debugDashDestination.transform.position = new(mousePosition.x, mousePosition.y);
#endif

        transform.position = new(mousePosition.x, mousePosition.y);
    }
    #endregion
}
