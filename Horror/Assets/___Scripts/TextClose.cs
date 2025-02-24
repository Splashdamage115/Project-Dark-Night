using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextClose : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction closePauseAction;

    public TextMeshProUGUI text;

    public PlayerInteraction playerPauseCheck;

    void Start()
    {
        //playerPauseCheck = GameObject.FindWithTag("Player").GetComponent<PlayerInteraction>();
        playerInput = GetComponent<PlayerInput>();

        closePauseAction = playerInput.actions["Pause"];
        closePauseAction.canceled += OnClose;
    }

    public void openPage(TextItem textItem)
    {
        playerPauseCheck.subMenuOpen = true;
        text.text = ReadTextFile.readFile(textItem.path);
        Time.timeScale = 0.0f;
    }

    void OnClose(InputAction.CallbackContext action)
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        playerPauseCheck.subMenuOpen = false;
    }
}
