using UnityEngine;

namespace KID
{
    /// <summary>
    /// 控制系統：敵人
    /// </summary>
    public class ControlSystemEnemy : ControlSystem
    {
        public bool checkPlayer => CheckPlayer();

        [SerializeField, Header("當前武器槍口")]
        private Transform weaponFirePoint;
        [Header("偵測玩家射線")]
        [SerializeField]
        private Color checkPlayerRayColor = new Color(1, 0.3f, 0.3f, 0.8f);
        [SerializeField, Range(1, 5)]
        private float checkPlayerRayLength;
        [SerializeField]
        private LayerMask checkPlayerRayLayer = 1 << 9;

        private Transform player;
        private float move;

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            if (weaponFirePoint == null) return;
            Gizmos.color = checkPlayerRayColor;
            Gizmos.DrawRay(
                weaponFirePoint.position, weaponFirePoint.right * checkPlayerRayLength);
        }

        protected override void Awake()
        {
            base.Awake();
            player = GameObject.Find(GameManager.playerName).transform;
        }

        protected override void Update()
        {
            base.Update();
            EnemyInput();
        }

        private void EnemyInput()
        {
            if (CheckPlayer())
            {
                rig.velocity = Vector3.zero;
                ani.SetFloat(parMove, 0);
                return;
            }
            move = player.position.x < transform.position.x ? -1 : +1;
            Move(move);
            Ladder(move);
        }

        private bool CheckPlayer()
        {
            RaycastHit2D hit = Physics2D.Raycast(
                weaponFirePoint.position, weaponFirePoint.right, checkPlayerRayLength, checkPlayerRayLayer);
            if (hit.collider == null) return false;

            return hit.collider.name.Equals(GameManager.playerName);
        }
    }
}
