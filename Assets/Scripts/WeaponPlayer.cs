using TMPro;
using UnityEngine;

namespace KID
{
    /// <summary>
    /// 武器：玩家輸入
    /// </summary>
    public class WeaponPlayer : MonoBehaviour
    {
        [Header("介面")]
        [SerializeField]
        private TMP_Text textWeaponName;
        [SerializeField]
        private TMP_Text textBulletCurrent;
        [SerializeField]
        private TMP_Text textBulletTotal;
        [SerializeField]
        private TMP_Text textMagazinePrice;

        private WeaponSystem weaponSystem;

        private void Awake()
        {
            weaponSystem = GetComponent<WeaponSystem>();
        }
    }
}
