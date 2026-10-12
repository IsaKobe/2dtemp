using UnityEngine;
using UnityEngine.InputSystem;

public class CustomMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] InputActionAsset inputActions;
    InputAction moveAction;
    InputAction logAction;

    private void Awake()
    {
        moveAction = inputActions.FindAction("Movement");
        logAction = inputActions.FindAction("Log");
    }

    private void OnEnable()
    {
        moveAction.Enable();
        logAction.Enable();

        logAction.performed += LogAction_performed;
    }

    private void LogAction_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("action triggered" + obj.action.name);
    }

    private void OnDisable()
    {
        moveAction.Disable();
        logAction.Disable();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector = moveAction.ReadValue<Vector2>() *(Time.deltaTime * speed);
        transform.position += vector;
    }
}
