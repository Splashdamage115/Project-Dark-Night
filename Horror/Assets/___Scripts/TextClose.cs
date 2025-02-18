using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextClose : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction closePauseAction;

    public TextMeshProUGUI text;


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        closePauseAction = playerInput.actions["Pause"];
        closePauseAction.canceled += OnClose;
    }

    public void openPage(TextItem textItem)
    {
        text.text = ReadTextFile.readFile(textItem.path);
        Time.timeScale = 0.0f;
    }

    void OnClose(InputAction.CallbackContext action)
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
