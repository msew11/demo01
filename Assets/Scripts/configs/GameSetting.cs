using UnityEngine;

namespace configs
{
    /**
     * 游戏设定
     */
    [CreateAssetMenu(fileName = "GameSetting", menuName = "Configs/GameSetting")]
    public class GameSetting : ScriptableObject
    {
        [Header("Mouse Settings")]
        [Range(0f, 1f)]
        public float mouseSensitivity = 0.2f;
    }
}