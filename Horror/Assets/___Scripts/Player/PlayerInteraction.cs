using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public Animator animator;

    private PlayerInput playerInput;
    private InputAction interactAction;
    private InputAction inventoryAction;
    private InputAction torchAction;

    public GameObject Inventory;
    public GameObject Torch;
    public InventoryItem torchItem;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        interactAction = playerInput.actions["Interact"];
        interactAction.performed += OnInteract;

        inventoryAction = playerInput.actions["Inventory"];
        inventoryAction.canceled += OnInventory;
        Inventory.SetActive(false);

        torchAction = playerInput.actions["Torch"];
        torchAction.canceled += OnTorch;
        Torch.SetActive(false);
    }
    void OnInteract(InputAction.CallbackContext action)
    {
        Camera.main.SendMessage("Interact", animator);
        animator.SetBool("AttemptInteract", true);
        StartCoroutine(resetInteractValues());
    }
    void OnInventory(InputAction.CallbackContext action)
    {
        Inventory.SetActive(!Inventory.activeSelf);

        if (Inventory.activeSelf == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Camera.main.SendMessage("setMoveState", true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Camera.main.SendMessage("setMoveState", false);
        }
    }

    void OnTorch(InputAction.CallbackContext action)
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null)
        {
            if (go.GetComponent<Inventory>().checkItem(torchItem))
            {
                Torch.SetActive(!Torch.activeSelf);
                animator.SetBool("Flashlight", Torch.activeSelf);
            }
        }
    }

    IEnumerator resetInteractValues()
    {
        yield return new WaitForNextFrameUnit();
        animator.SetBool("AttemptInteract", false);
        animator.SetBool("InteractValid", false);
        animator.SetBool("InteractPush", false);
        yield return null;
    }
}
