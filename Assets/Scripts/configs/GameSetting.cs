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
        public float mouseSensitivity = 2.0f;
    }
}