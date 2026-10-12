using UnityEngine;
using UnityEngine.InputSystem;

public class CustomMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] InputActionAsset inputActions;
    InputAction moveAction;
    InputAction logAction;

    InputAction mouseAction;
    Rigidbody2D rigidbody2;
    private void Awake()
    {
        moveAction = inputActions.FindAction("Movement");
        logAction = inputActions.FindAction("Log");
        mouseAction = inputActions.FindAction("Mouse");

        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        moveAction.Enable();
        logAction.Enable();
        mouseAction.Enable();

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
        mouseAction.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 vector = Camera.main.ScreenToWorldPoint(mouseAction.ReadValue<Vector2>());
        vector = Vector2.MoveTowards(transform.position, vector, speed);
        transform.position = vector;*/
        //Vector3 vector = ;
        //transform.position += vector;
        //transform.Translate(vector);
    }
    
    private void FixedUpdate()
    {
        Vector2 newPos = rigidbody2.position + (moveAction.ReadValue<Vector2>() * speed);
        rigidbody2.MovePosition(newPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("narazil jsem do zdi");
        }
        else
        {
            Debug.Log("Co to bylo?");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("prošel jsem zdí jsem do zdi");
    }
}
