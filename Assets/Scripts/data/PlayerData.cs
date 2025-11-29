using UnityEngine;

namespace data
{
    public class PlayerData
    {
        public string PlayerId;

        public bool IsGround = true;
        public Vector3 Velocity = Vector3.zero;

        public PlayerData(string playerId)
        {
            PlayerId = playerId;
        }
    }
}