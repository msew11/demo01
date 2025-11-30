namespace data
{
    public class LocalPlayerData
    {
        public readonly long EntityId;
        public readonly string PlayerId;

        public LocalPlayerData(long entityId, string playerId)
        {
            EntityId = entityId;
            PlayerId = playerId;
        }
    }
}