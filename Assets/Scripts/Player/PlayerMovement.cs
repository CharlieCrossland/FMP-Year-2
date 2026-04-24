using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [Header("References")]
    public Rigidbody2D rb;
    public GameObject dashOutline;
    [SerializeField] private Slider slider;

    [Header("Editables")]
    public float moveSpeed;
    public float maxDashDistance;
    private float slowTimer;
    public float maxSlowTimer;
    public float dashSlowTimeScale;

    [Header("Miscalleneous")]
    private Vector3 currentMovement;

    [Header("Dashing")]
    private bool dashSlowActive;
    private bool hasSlowBeenUsed;
    private bool canDash;
    public float dashBarAmount;

    [HideInInspector] public bool canMove;

    private Vector3 mousePosition;

    public bool icyFloor;

    bool isFacingRight;

    private void Awake()
    {
        Instance = this;

        slowTimer = maxSlowTimer;
        isFacingRight = true;

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (GameStatesManager.Instance.currentState == GameStatesManager.GameStates.GameOver)
        {

        }
        else
        {
            MovementDirection();
            DashOutline();
        }
    }

    void FixedUpdate()
    {
        if (GameStatesManager.Instance.currentState == GameStatesManager.GameStates.GameOver)
        {

        }
        else
        {
            DashBar();
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        currentMovement = PlayerInputHandler.Instance.MovementInput * Time.fixedDeltaTime * moveSpeed;

        if (!icyFloor)
        {
            rb.linearDamping = 0f;

            rb.MovePosition(rb.position + (Vector2)currentMovement);
        }
        else
        {
            rb.linearDamping = 1.5f;

            rb.linearVelocity += Vector2.ClampMagnitude((Vector2)currentMovement, moveSpeed);
        }
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

    void DashHandler()
    {
        if (PlayerInputHandler.Instance.DashTriggered)
        {
            // preventing the slow being permenant
            // if the slow hasnt started then activate the slow
            if (hasSlowBeenUsed == false)
            {
                dashSlowActive = true;
            }

            // shows the player where they will be if they let go and dash
            dashOutline.SetActive(true);

            // this is to check if letting go will dash the player
            canDash = true;
        }
        else // using an else statement so this only happens after dash input has been triggered
        {
            CheckDash();

            dashOutline.SetActive(false);

            canDash = false;
        }

        // checking if the dash slow should be happening
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
        // a timer is started when dash is held
        // when timer reaches 0, the slow effect is disabled
        // the player can still dash after the slow is disabled
        if (slowTimer <= 0)
        {
            ResetTimeScale();
            dashSlowActive = false;
            hasSlowBeenUsed = true;
            slowTimer = maxSlowTimer;
        }
        else if (dashSlowActive)
        {
            // use unscaled time so that the timer goes down as usual without being slowed
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
        // canDash is set to true when the dash input is set to true, so check for dash input being set to false (letting go of dash button)
        // when this happens everything is reset
        if (canDash)
        {
            if (PlayerInputHandler.Instance.DashTriggered == false)
            {
                dashSlowActive = false;
                slowTimer = maxSlowTimer;
                hasSlowBeenUsed = false;
                dashBarAmount = 0;

                TriggerDash();
                Debug.Log("Player Dash");
            }
        }
    }

    void DashBar()
    {
        DashBarUI();

        if (dashBarAmount >= 5)
        {
            DashHandler();
        }
        else
        {

        }
    }

    void DashBarUI()
    {
        slider.value = dashBarAmount;
    }

    void TriggerDash()
    {
        // sets the players position directly onto the mouses x and y
        // do not set z as this creates issues
        transform.position = new(mousePosition.x, mousePosition.y);
    }
    #endregion
}
