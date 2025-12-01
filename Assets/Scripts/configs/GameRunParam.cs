using UnityEngine;

namespace configs
{
    /**
     * 游戏运行参数
     */
    [CreateAssetMenu(fileName = "GameRunParam", menuName = "Configs/GameRunParam")]
    public class GameRunParam : ScriptableObject
    {
        [Header("Movement Parameters")]
        public float speed = 10f;

        [Header("Physics Parameters")]
        public LayerMask groundLayer;
        [Range(-30f, -1f)]
        public float gravity = -9.8f;
        [Range(-0.2f, -0.1f)]
        public float groundCheckRadius = 0.2f;
        public float jumpHeight = 3f;
    }
}