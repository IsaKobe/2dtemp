using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomMovement : MonoBehaviour
{
    private static readonly int IsHangHash = Animator.StringToHash("IsHang");
    private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
    private static readonly int IsFlyingHash = Animator.StringToHash("IsFlying");
    [SerializeField] float speed;
    [SerializeField] InputActionAsset inputActions;
    InputAction moveAction; 
    InputAction jumpAction;
    InputAction mouseAction;
    Rigidbody2D rigidbody2;

    Animator animator;
    SpriteRenderer renderer;

    ContactPoint2D[] contacts;
    private void Awake()
    {
        moveAction = inputActions.FindAction("Movement");
        jumpAction = inputActions.FindAction("Log");
        mouseAction = inputActions.FindAction("Mouse");

        animator = GetComponent<Animator>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        contacts = new ContactPoint2D[5];
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        mouseAction.Enable();

        jumpAction.performed += Jump;
    }

    bool orderedJump = false;
    private void Jump(InputAction.CallbackContext obj)
    {
        orderedJump = true;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        mouseAction.Disable();
        jumpAction.performed -= Jump;
    }

    [SerializeField] float jumpStrength;
    [SerializeField] float gravitySpeed;
    [SerializeField] float verticalVelocity = 0;
    [SerializeField] float verticalSpeed;
    [SerializeField] float gravitySpeedSlowed;
    bool wasGrounded;

    private void FixedUpdate()
    {
        float input = moveAction.ReadValue<float>();


        bool _onGround = Contacts();
        Jump(_onGround);
        HandleAnims(input);
        

        renderer.flipX = input < 0;

        Vector2 newPos = rigidbody2.position +
            new Vector2(
                input * speed,
                verticalVelocity * verticalSpeed);

        rigidbody2.MovePosition(newPos);
        wasGrounded = _onGround;
    }

    private void HandleAnims(float input)
    {
        if (!Mathf.Approximately(verticalVelocity, 0))
        {
            animator.SetBool(IsFlyingHash, true);
        }
        else
        {
            animator.SetBool(IsHangHash, false);
            animator.SetBool(IsFlyingHash, false);
            animator.SetBool(IsWalkingHash, input != 0);
        }
    }


    private bool Contacts()
    {
        int hitNumber = rigidbody2.GetContacts(contacts);
        bool _onGround = false;
        for (int i = 0; i < hitNumber; i++)
        {
            if (contacts[i].normal.y == 1)
            {
                _onGround = true;
                break;
            }
            else if (contacts[i].normal.y == -1)
                verticalVelocity = 0;

        }

        return _onGround;
    }
    private void Jump(bool _onGround)
    {
        if (_onGround)
        {
            if (!wasGrounded)
            {
                verticalVelocity = 0;
            }
            else if (orderedJump)
            {
                verticalVelocity = jumpStrength;
            }
        }
        else
        {
            verticalVelocity -= gravitySpeed;
        }
        orderedJump = false;
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
