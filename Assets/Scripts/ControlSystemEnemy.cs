using UnityEngine;

namespace KID
{
    /// <summary>
    /// 控制系統：敵人
    /// </summary>
    public class ControlSystemEnemy : ControlSystem
    {
        private Transform weaponFirePoint;

        [Header("偵測玩家射線")]
        [SerializeField]
        private Color checkPlayerRayColor = new Color(0.5f, 1, 0.5f, 0.7f);
        [SerializeField, Range(0, 15)]
        private float checkPlayerLength = 3.5f;
        [SerializeField]
        private LayerMask checkPlayerLayer = 1 << 3 | 1 << 6;
    }
}
