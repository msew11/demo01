#if UNITY_EDITOR
using System.Reflection;
using data;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)] // true表示也适用于继承类
public class PlayerControllerBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // 检查目标对象是否为PlayerController或其子类
        if (target is MonoBehaviour playerController)
        {
            // 通过反射获取 _playerData 字段
            var playerDataField = target.GetType().GetField("_playerData",
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (playerDataField != null)
            {
                var playerData = (PlayerData)playerDataField.GetValue(target);
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
}
#endif