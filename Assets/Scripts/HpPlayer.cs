using UnityEngine;

namespace KID
{
    /// <summary>
    /// 玩家血量
    /// </summary>
    public class HpPlayer : HpSystem
    {
        protected override void Dead()
        {
            base.Dead();
            GameManager.instance.ShowFinalUI("挑戰失敗");
        }
    }
}
