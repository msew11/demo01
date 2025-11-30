using UnityEngine;

namespace data
{
    public class MoveData: BaseData
    {
        public bool IsGround = true;
        public Vector3 Velocity = Vector3.zero;

        public Vector3 Position = Vector3.zero;
        public bool IsDirty = false;
        public Vector3 Dir = Vector3.zero;
    }
}