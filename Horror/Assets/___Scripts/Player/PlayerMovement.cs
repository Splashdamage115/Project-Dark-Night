using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
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

    [SerializeField]
    private float SprintMultiplier = 100f;
    private float currentMultiplier = 1.0f;
    bool sprinting = false;
    [SerializeField]
    private float maxSprintTime = 2.0f;
    private float currentSprintTime = 0f;
    [SerializeField]
    private float recoverMultiplier = 0.5f;

    public Scrollbar sprintBar;
    [SerializeField]
    private float dissapearTime = 1.0f;
    private float currentDissapearTime = 0f;

    void Start()
    {
        currentDissapearTime = dissapearTime;
        currentSprintTime = maxSprintTime;

        rb = GetComponent<Rigidbody>();

        // bind C# scripts
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["Sprint"];
        
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        jumpAction.performed += OnJump;
        sprintAction.performed += sprint;
        sprintAction.canceled += stopSprint;
    }
    void sprint(InputAction.CallbackContext action)
    {
        activateSprint();
    }
    void stopSprint(InputAction.CallbackContext action)
    {
        deactivateSprint();
    }
    void activateSprint()
    {
        currentDissapearTime = dissapearTime;
        sprintBar.gameObject.SetActive(true);
        sprinting = true;
        currentMultiplier = SprintMultiplier;
    }
    void deactivateSprint()
    {
        sprinting = false;
        currentMultiplier = 1.0f;
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
        if(sprinting)
        {
            if (currentSprintTime >= 0f)
            {
                currentSprintTime -= Time.deltaTime;
                if(currentSprintTime <= 0f)
                {
                    deactivateSprint();
                }
            }
        }
        else
        {
            currentSprintTime += Time.deltaTime * recoverMultiplier;
            if(currentSprintTime > maxSprintTime)
            {
                currentSprintTime = maxSprintTime;
                if (currentDissapearTime >= 0f)
                {
                    currentDissapearTime -= Time.deltaTime;
                    if (currentDissapearTime <= 0f)
                        sprintBar.gameObject.SetActive(false);
                }

            }
        }
        sprintBar.size = currentSprintTime / maxSprintTime;

        Vector3 move = new Vector3(movement.x, jump, movement.y);
        rb.velocity = transform.TransformDirection(move * speed * Time.deltaTime * 10f * currentMultiplier);

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
