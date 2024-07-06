using UnityEngine;

namespace KID
{
    /// <summary>
    /// 控制系統
    /// </summary>
    public class ControlSystem : MonoBehaviour
    {
        // SerializeField 序列化，將變數顯示在面板
        // Header 標題，在變數上顯示文字
        // Range(最小，最大) 設定變數範圍限制
        [SerializeField, Header("移動速度"), Range(0, 10)]
        private float moveSpeed = 3.5f;
        [SerializeField, Header("爬梯速度"), Range(0, 10)]
        private float ladderSpeed = 1.5f;
        [SerializeField, Header("檢查階梯顏色")]
        private Color ladderColor = new Color(1, 0.3f, 0.3f, 0.65f);
        [SerializeField, Header("檢查階梯尺寸")]
        private Vector3 ladderSize;
        [SerializeField, Header("檢查階梯位移")]
        private Vector3 ladderOffset;
        [SerializeField, Header("檢查階梯圖層")]
        private LayerMask ladderLayer = 1 << 3;

        private Rigidbody2D rig;
        private Animator ani;
        private string parMove = "移動數值";
        private string parDirection = "方向數值";

        private void OnDrawGizmos()
        {
            Gizmos.color = ladderColor;
            Gizmos.DrawCube(
                transform.position + ladderOffset,
                ladderSize);
        }

        private void Awake()
        {
            // 獲得此物件身上的 2D 剛體並存放到變數 rig 內
            rig = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();
        }

        private void Update()
        {
            Move();
            Ladder();
        }

        private void Move()
        {
            float h = Input.GetAxis("Horizontal");
            ani.SetFloat(parDirection, h);
            rig.velocity = new Vector2(h * moveSpeed, rig.velocity.y);
            h = Mathf.Abs(h);
            ani.SetFloat(parMove, h);
        }

        private void Ladder()
        {
            float h = Input.GetAxis("Horizontal");

            Collider2D hit = Physics2D.OverlapBox(
                transform.position + ladderOffset,
                ladderSize, 0, ladderLayer);

            if (hit == null) return;
            if (Mathf.Abs(h) < 0.2f) return;

            rig.velocity = new Vector2(rig.velocity.x, ladderSpeed);
        }
    }
}
