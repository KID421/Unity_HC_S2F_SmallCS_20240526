using UnityEngine;

namespace KID
{
    /// <summary>
    /// 控制系統：敵人
    /// </summary>
    public class ControlSystemEnemy : ControlSystem
    {
        [SerializeField, Header("子彈生成位置")]
        private Transform spawnBulletPoint;
        [SerializeField, Header("射線長度"),Range(0, 30)]
        private float rayLength = 3;
        [SerializeField, Header("目標圖層")]
        private LayerMask layerTarget = 1 << 6 | 1 << 3;

        private Transform playerTransform;
        private int direction => playerTransform.position.x < transform.position.x ? -1 : 1;

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.green;
            Gizmos.DrawRay(
                spawnBulletPoint.position, 
                spawnBulletPoint.right * rayLength);
        }

        protected override void Awake()
        {
            base.Awake();
            playerTransform = GameObject.Find(GameManager.playerName).transform;
        }

        private void Update()
        {
            EnemyInput();
        }

        protected void EnemyInput()
        {
            float speed = IsRayHitPlayer() ? 0 : 1;
            Move(speed * direction);
            Ladder(speed);
        }

        private bool IsRayHitPlayer()
        {
            RaycastHit2D hit = Physics2D.Raycast(spawnBulletPoint.position,
                spawnBulletPoint.right, rayLength, layerTarget);
            if (hit.collider == null) return false;
            bool isPlayer = hit.collider.gameObject.name.Contains(GameManager.playerName);
            return isPlayer;
        }
    }
}
