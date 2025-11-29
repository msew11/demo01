#if UNITY_EDITOR
using System.Reflection;
using data;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerController playerController = (PlayerController)target;

        // 通过反射获取 _playerData 字段（假设是 Dictionary 类型）
        var playerDataField = typeof(PlayerController).GetField("_playerData",
            BindingFlags.NonPublic | BindingFlags.Instance);

        if (playerDataField != null)
        {
            var playerData = (PlayerData)playerDataField.GetValue(playerController);
            if (playerData != null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Player Data", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("PlayerId", playerData.PlayerId);
                EditorGUILayout.LabelField("IsGround", playerData.IsGround.ToString());
                EditorGUILayout.LabelField("Velocity", playerData.Velocity.ToString());
            }
            else
            {
                EditorGUILayout.LabelField("Player Data", "Not initialized yet");
            }
        }
    }
}
#endif