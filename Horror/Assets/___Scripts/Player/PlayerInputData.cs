using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputDataMenu", menuName = "Input Data")]
public class PlayerInputData : ScriptableObject
{
    public bool invertYAxis = false;
    public int xSensitivity = 50;
    public int ySensitivity = 50;
}
