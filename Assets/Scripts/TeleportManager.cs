using UnityEngine;

namespace KID
{
    /// <summary>
    /// 傳送陣管理器
    /// </summary>
    public class TeleportManager : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name.Equals(GameManager.playerName))
            {
                GameManager.instance.ShowFinalUI("挑戰成功");
            }
        }
    }
}
