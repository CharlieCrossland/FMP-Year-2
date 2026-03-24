using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    // this allows for the script to be referenced anywhere, use PlayerInputHandler.Instance
    public static PlayerInputHandler Instance;

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string movement = "Movement";
    [SerializeField] private string rotation = "Rotation";
    [SerializeField] private string attack = "Attack";
    [SerializeField] private string dash = "Dash";
    [SerializeField] private string smartBomb = "Smart Bomb";

    [Header("Input Actions")]
    private InputAction movementAction;
    private InputAction rotationAction;
    private InputAction attackAction;
    private InputAction dashAction;
    private InputAction smartBombAction;


    public Vector2 MovementInput { get; private set; }
    public Vector2 RotationInput { get; private set; }
    public bool AttackTriggered { get; private set; }
    public bool DashTriggered { get; private set; }
    public bool SmartBombTriggered { get; private set; }


    private void Awake()
    {
        Instance = this;

        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);

        movementAction = mapReference.FindAction(movement);
        rotationAction = mapReference.FindAction(rotation);
        attackAction = mapReference.FindAction(attack);
        dashAction = mapReference.FindAction(dash);;
        smartBombAction = mapReference.FindAction(smartBomb);

        SubscribeActionValuesToInputEvents();
    }

    private void SubscribeActionValuesToInputEvents()
    {
        movementAction.performed += inputInfo => MovementInput = inputInfo.ReadValue<Vector2>();
        movementAction.canceled += inputInfo => MovementInput = Vector2.zero;

        rotationAction.performed += inputInfo => RotationInput = inputInfo.ReadValue<Vector2>();
        rotationAction.canceled += inputInfo => RotationInput = Vector2.zero;

        attackAction.performed += inputInfo => AttackTriggered = true;
        attackAction.canceled += inputInfo => AttackTriggered = false;

        dashAction.performed += inputInfo => DashTriggered = true;
        dashAction.canceled += inputInfo => DashTriggered = false;

        smartBombAction.performed += inputInfo => SmartBombTriggered = true;
        smartBombAction.canceled += inputInfo => SmartBombTriggered = false;
    }

    private void OnEnable()
    {
        playerControls.FindActionMap(actionMapName).Enable();
    }

    private void OnDisable()
    {
        playerControls.FindActionMap(actionMapName).Disable();
    }
}