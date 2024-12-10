using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovementController : MonoBehaviour
{
    public GameObject player;
    Vector2 mouseMovement;
    float xRotation, yRotation;
    float cameraOffset;

    public PlayerInputData playerInputData;

    void Start()
    {
        cameraOffset = transform.position.y - player.transform.position.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y += cameraOffset;
        transform.position = pos;

        mouseMovement = Mouse.current.delta.ReadValue();
        if(playerInputData.invertYAxis)
            xRotation += mouseMovement.y * Time.deltaTime * playerInputData.ySensitivity;
        else
            xRotation += -mouseMovement.y * Time.deltaTime * playerInputData.ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        yRotation += mouseMovement.x * Time.deltaTime * playerInputData.xSensitivity;
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        player.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
