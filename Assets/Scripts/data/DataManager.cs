using System.Collections.Generic;

namespace data
{
    public class DataManager
    {
        public static DataManager Instance { get; } = new DataManager();

        public readonly LocalPlayerData LocalPlayerData = new LocalPlayerData();
        private readonly Dictionary<string, PlayerData> _playerDataMap;

        private DataManager()
        {
            _playerDataMap = new Dictionary<string, PlayerData>();
        }

        // 添加玩家数据
        public void AddPlayerData(PlayerData playerData)
        {
            _playerDataMap[playerData.PlayerId] = playerData;
        }

        // 获取玩家数据
        public PlayerData GetPlayerData(string playerId)
        {
            return _playerDataMap.GetValueOrDefault(playerId);
        }

        // 移除玩家数据
        public void RemovePlayerData(string playerId)
        {
            _playerDataMap.Remove(playerId);
        }

    }
}