using UnityEngine;

namespace KID
{
    /// <summary>
    /// 武器旋轉：敵人
    /// </summary>
    public class WeaponRotateEnemy : WeaponRotate
    {
        private Transform playerTransform;

        private void Awake()
        {
            playerTransform = GameObject.Find(GameManager.playerName).transform;
            crossHair = new GameObject("敵人_準心").transform;
        }

        protected override void Update()
        {
            CrossHairPosition();
            base.Update();
        }

        private void CrossHairPosition()
        {
            crossHair.position = playerTransform.position;
        }
    }
}
