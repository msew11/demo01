#if UNITY_EDITOR
using system;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerController playerController = (PlayerController)target;

        // 获取 gravitySystem 实例
        var gravitySystemField = typeof(PlayerController).GetField("_gravitySystem",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (gravitySystemField != null)
        {
            var gravitySystem = gravitySystemField.GetValue(playerController) as GravitySystem;
            if (gravitySystem != null)
            {
                // 直接访问私有字段 _velocity
                var velocityField = typeof(GravitySystem).GetField("_velocity",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (velocityField != null)
                {
                    Vector3 velocity = (Vector3)velocityField.GetValue(gravitySystem);
                    EditorGUILayout.LabelField("Gravity Velocity", velocity.ToString());
                }
            }
        }
    }
}
#endif