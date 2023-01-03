using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    Vector2 moveInput;
    public float CurrentWalkSpeed {
        get 
        {
            if (isMoving)
            {
                if (IsRunning) 
                {
                    return runSpeed;
                }
                else 
                {
                    return walkSpeed;
                }
            }
            else
            {
                // Idle speed
                return 0f;
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool isMoving { 
        get
        {
            return _isMoving;
        } 
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    [SerializeField]
    private bool _isRunning;

    public bool IsRunning {
        get
        {
            return _isRunning;
        }
        private set {
            _isRunning = value;
            animator.SetBool("isRunning", value);
        }
    }

    private bool _isFacingRight = true;

    public bool IsFacingRight { 
        get
        {
            return _isFacingRight;
        }
        private set {
            if (_isFacingRight != value)
            {
                // Flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    Rigidbody2D rb;

    Animator animator;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate() {
        rb.velocity = new Vector2(moveInput.x * CurrentWalkSpeed, rb.velocity.y);
    }

    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();        

        isMoving = moveInput != Vector2.zero;
        
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight) {
            // Face the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight) {
            // Face the left
            IsFacingRight = false;
        }
    }

    public void onRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
}
