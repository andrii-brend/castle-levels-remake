using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset _actionAsset;
    [Header("Action map referenses")]
    [SerializeField] private string _inputActionsMap = "Default";

    [Header("Action Name")]
    [SerializeField] private string _moveActionName = "Move";
    [SerializeField] private string _jumpActionName = "Jump";

    private InputAction _moveAction;
    private InputAction _jumpAction;

    public Vector2 Move { get; private set; }
    public bool isJump { get; private set; }

    public static InputHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _moveAction = _actionAsset.FindActionMap(_inputActionsMap).FindAction(_moveActionName);
        _jumpAction = _actionAsset.FindActionMap(_inputActionsMap).FindAction(_jumpActionName);
        RegisterActions();
    }

    private void RegisterActions()
    {
        _moveAction.performed += context => Move = context.ReadValue<Vector2>();
        _moveAction.canceled += context => Move = context.ReadValue<Vector2>();

        _jumpAction.performed += context => isJump = true;
        _jumpAction.canceled += context => isJump = false;
    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _jumpAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _jumpAction.Disable();
    }
}
