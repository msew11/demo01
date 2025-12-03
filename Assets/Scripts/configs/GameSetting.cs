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
        public float mouseXSensitivity = 0.2f;
        [Range(0f, 1f)]
        public float mouseYSensitivity = 0.2f;
    }
}