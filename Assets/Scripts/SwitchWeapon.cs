using UnityEngine;

namespace KID
{
    /// <summary>
    /// 切換武器
    /// </summary>
    public class SwitchWeapon : MonoBehaviour
    {
        // 陣列：儲存多筆相同類型的資料
        [SerializeField, Header("所有武器")]
        private GameObject[] weapons;
        [SerializeField, Header("所有武器的切換按鍵")]
        private KeyCode[] weaponKeys =
        {
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4
        };
    }
}
