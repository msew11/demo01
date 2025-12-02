using UnityEngine;

namespace data
{
    public class MoveData: BaseData
    {
        // 是否接触地面
        public bool IsGround = true;
        // 竖直方向的速度
        public Vector3 UpVelocity = Vector3.zero;

        public Vector3 Position = Vector3.zero;
        public Vector3 Dir = Vector3.zero;
    }
}