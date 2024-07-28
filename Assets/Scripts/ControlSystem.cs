using UnityEngine;

namespace KID
{
    /// <summary>
    /// 控制系統
    /// </summary>
    public class ControlSystem : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("移動速度"), Range(0, 10)]
        private float moveSpeed = 3.5f;
        [SerializeField, Header("爬梯速度"), Range(0, 10)]
        private float ladderSpeed = 3.5f;
        // Color(紅，綠，藍，透明度) 值：0 ~ 1 (百分比)
        [SerializeField, Header("爬梯區域顏色")]
        private Color ladderColor = new Color(1, 0.3f, 0.3f, 0.7f);
        [SerializeField, Header("爬梯區域尺寸")]
        private Vector3 ladderSize;
        [SerializeField, Header("爬梯區域位移")]
        private Vector3 ladderOffset;
        [SerializeField, Header("爬梯區域圖層")]
        private LayerMask ladderLayer = 1 << 3;

        private Rigidbody2D rig;
        private Animator ani;
        private string parMove = "移動數值";
        #endregion

        #region 事件
        // ODG 繪製圖示事件，在編輯器內繪製提示圖示
        protected virtual void OnDrawGizmos()
        {
            // 決定圖示顏色
            Gizmos.color = ladderColor;
            // 決定圖示形狀(座標，尺寸)
            // transform.position 此物件的座標
            Gizmos.DrawCube(transform.position + ladderOffset, ladderSize);
        }

        protected virtual void Awake()
        {
            // 獲得此物件身上的 2D 剛體並存放到變數 rig 內
            rig = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();
        }
        #endregion

        #region 方法
        protected void Move(float speed)
        {
            
            // 剛體的加速度 = 玩家水平按鍵 * 移動速度，Y 軸是原本的重力
            rig.velocity = new Vector2(speed * moveSpeed, rig.velocity.y);
            // 對 h 取絕對值
            speed = Mathf.Abs(speed);
            // 設定浮點數參數 為 h
            ani.SetFloat(parMove, speed);
        }

        protected void Ladder(float speed)
        {
            // 2D 物理.覆蓋立方體(座標，尺寸，角度，圖層)
            Collider2D hit = Physics2D.OverlapBox(transform.position + ladderOffset,
                ladderSize, 0, ladderLayer);

            // 如果 hit 是空的 就不執行下面的程式 (跳出)
            if (hit == null) return;
            if (Mathf.Abs(speed) < 0.2f) return;

            rig.velocity = new Vector2(rig.velocity.x, ladderSpeed);
        } 
        #endregion
    }
}
