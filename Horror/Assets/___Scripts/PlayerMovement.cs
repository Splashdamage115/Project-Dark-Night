using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Vector2 movement;

    [SerializeField]
    private float speed;
    private Rigidbody rb;

    private bool grounded = true;
    [SerializeField]
    private float jumpHeight = 10.0f;
    [SerializeField]
    private float jumpDecrease = 1.0f;
    private float jump = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // bind C# scripts
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        jumpAction.performed += OnJump;
    }

    void OnMove(InputAction.CallbackContext action)
    {
        movement = action.ReadValue<Vector2>();
    }

    void OnJump(InputAction.CallbackContext action)
    {
        if (grounded)
        {
            jump = jumpHeight;
            grounded = false;
        }
    }

    void Update()
    {
        Vector3 move = new Vector3(movement.x, jump, movement.y);
        rb.velocity = transform.TransformDirection(move * speed * Time.deltaTime * 10);

        if (!grounded)
        {
            jump -= jumpDecrease;
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit info;
            if (Physics.Raycast(ray, out info))
            {
                if (info.distance <= 1.0f)
                {
                    if (info.collider.gameObject.CompareTag("Ground"))
                    {
                        grounded = true;
                        jump = 0.0f;
                    }
                }
            }
        }

        

        }
}
