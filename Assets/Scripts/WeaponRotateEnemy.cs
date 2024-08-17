using UnityEngine;

namespace KID
{
    /// <summary>
    /// 武器旋轉：敵人
    /// </summary>
    public class WeaponRotateEnemy : WeaponRotate
    {
        private void Awake()
        {
            crossHair = GameObject.Find(GameManager.playerName).transform;
        }
    }
}
