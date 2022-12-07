using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;

    public bool isMoving { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();        

        isMoving = moveInput != Vector2.zero;
    }
}
