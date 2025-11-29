using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game Config")]
public class GameConfig : ScriptableObject
{
    [Header("Movement Settings")]
    public float speed = 10f;

    [Header("Camera Settings")]
    public float mouseSensitivity = 2.0f;
}