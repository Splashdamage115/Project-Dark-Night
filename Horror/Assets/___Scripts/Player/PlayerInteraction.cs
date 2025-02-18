using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    private InputAction weaponAction;
    private InputAction fireAction;
    private InputAction pauseAction;

    public GameObject Inventory;
    public GameObject Torch;
    public InventoryItem torchItem;

    public GameObject Weapon;
    public InventoryItem PistolItem;

    public float MaxshootCooldown = 0.5f;
    private float shootCooldown = 0.0f;

    public bool subMenuOpen = false;
    public GameObject pauseMenu;

    void Start()
    {
        shootCooldown = MaxshootCooldown;

        playerInput = GetComponent<PlayerInput>();

        interactAction = playerInput.actions["Interact"];
        interactAction.performed += OnInteract;

        inventoryAction = playerInput.actions["Inventory"];
        inventoryAction.canceled += OnInventory;
        Inventory.SetActive(false);

        torchAction = playerInput.actions["Torch"];
        torchAction.canceled += OnTorch;
        Torch.SetActive(false);

        weaponAction = playerInput.actions["Gun"];
        weaponAction.canceled += OnWeapon;
        Weapon.SetActive(false);

        weaponAction = playerInput.actions["Gun"];
        weaponAction.canceled += OnWeapon;
        Weapon.SetActive(false);

        fireAction = playerInput.actions["Fire"];
        fireAction.canceled += OnShoot;

        pauseAction = playerInput.actions["Pause"];
        pauseAction.canceled += OnPause;
    }

    private void Update()
    {
        if(shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }
    void OnPause(InputAction.CallbackContext action)
    {
        if(!subMenuOpen)
            pauseMenu.gameObject.SetActive(!pauseMenu.activeSelf);
    }
    
    void OnInteract(InputAction.CallbackContext action)
    {
        if(pauseMenu.activeSelf == false) { 
        Camera.main.SendMessage("Interact", animator);
        animator.SetBool("AttemptInteract", true);
        StartCoroutine(resetInteractValues());
        }
    }
    void OnInventory(InputAction.CallbackContext action)
    {
        if (pauseMenu.activeSelf == false)
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
    }

    void OnTorch(InputAction.CallbackContext action)
    {
        if (pauseMenu.activeSelf == false)
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
    }

    void OnWeapon(InputAction.CallbackContext action)
    {
        if (pauseMenu.activeSelf == false)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            if (go != null)
            {
                if (go.GetComponent<Inventory>().checkItem(PistolItem))
                {
                    animator.SetBool("PistolHeld", !Weapon.activeSelf);
                    StartCoroutine(changeWeaponActive());
                }
            }
        }
    }

    void OnShoot(InputAction.CallbackContext action)
    {
        if (pauseMenu.activeSelf == false)
        {
            if (Weapon.activeSelf)
            {
                if (shootCooldown <= 0.0f)
                {
                    shootCooldown = MaxshootCooldown;
                    animator.SetBool("Shoot", true); StartCoroutine(resetShoot());
                    Weapon.GetComponent<ShootWeapon>().shoot();
                    Camera.main.SendMessage("hitScan", Weapon.GetComponent<ShootWeapon>().damageAmount);
                }
            }
        }
    }

    IEnumerator changeWeaponActive()
    {
        yield return new WaitForSeconds(0.2f);
        Weapon.SetActive(!Weapon.activeSelf);
        yield return null;
    }

    IEnumerator resetInteractValues()
    {
        yield return new WaitForNextFrameUnit();
        animator.SetBool("AttemptInteract", false);
        animator.SetBool("InteractValid", false);
        animator.SetBool("InteractPush", false);
        yield return null;
    }

    IEnumerator resetShoot()
    {
        yield return new WaitForNextFrameUnit();
        animator.SetBool("Shoot", false);
        yield return null;
    }
}
