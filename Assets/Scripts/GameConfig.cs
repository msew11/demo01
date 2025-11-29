using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game Config")]
public class GameConfig : ScriptableObject
{
    [Header("Movement Settings")]
    public float speed = 10f;

    [Header("Physics Settings")]
    public LayerMask groundLayer;
    [Range(-30f, -1f)]
    public float gravity = -9.8f;
    [Range(-0.2f, -0.1f)]
    public float groundCheckRadius = 0.2f;
    public float jumpHeight = 3f;

    [Header("Camera Settings")]
    public float mouseSensitivity = 2.0f;
}