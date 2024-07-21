using TMPro;
using UnityEngine;

namespace KID
{
    /// <summary>
    /// 武器系統
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField, Header("武器資料")]
        private DataWeapon dataWeapon;
        [SerializeField, Header("子彈生成位置")]
        private Transform spawnBulletPoint;
        [Header("介面")]
        [SerializeField]
        private TMP_Text textWeaponName;
        [SerializeField]
        private TMP_Text textBulletCurrent;
        [SerializeField]
        private TMP_Text textBulletTotal;
        [SerializeField]
        private TMP_Text textMagazinePrice;

        private int bulletCurrent;
        private int bulletTotal;

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            Fire();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            textWeaponName.text = dataWeapon.weaponName;
            textBulletCurrent.text = $"子彈：{dataWeapon.magazineBulletCount}";
            textBulletTotal.text = "總數：0";
            textMagazinePrice.text = $"價格：{dataWeapon.magazinePrice}";
            bulletCurrent = dataWeapon.magazineBulletCount;
            bulletTotal = 0;
        }

        private void Fire()
        {
            // 如果 按下左鍵 就 生成子彈
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // 生成(物件，座標，角度)
                // Quaternion.identity 零度角
                GameObject tempBullet = Instantiate(dataWeapon.bulletPrefab, spawnBulletPoint.position, Quaternion.identity);
                // 獲得生成子彈的 2D 剛體 並添加推力 往子彈生成位置前方 (X軸) 發射
                tempBullet.GetComponent<Rigidbody2D>().AddForce(spawnBulletPoint.right * dataWeapon.bulletSpeed);
            }
        }
    }
}
