using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

namespace KID
{
    /// <summary>
    /// 武器系統
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField, Header("武器資料")]
        protected DataWeapon dataWeapon;
        [SerializeField, Header("子彈生成位置")]
        protected Transform spawnBulletPoint;
        [SerializeField, Header("生成子彈數"), Range(1, 30)]
        protected int spawnBulletCount = 1;
        [SerializeField, Header("生成子彈前後位移"), Range(0, 2)]
        protected float spawnBulletOffsetX;

        protected int bulletCurrent;
        protected int bulletTotal;
        protected int magazineCount;
        // 能不能開槍，預設值為 true 代表一開始可以開槍
        private bool canFire = true;
        // 是否在換彈匣
        private bool isReload;

        protected Action bulletCountChange;

        protected virtual void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Initialize()
        {
            bulletCurrent = dataWeapon.magazineBulletCount;
            bulletTotal = 0;
        }

        protected virtual void Fire(bool fire)
        {
            // 如果 不能開槍 就 跳出
            if (!canFire) return;
            // 如果 目前子彈 <= 0 就 跳出
            if (bulletCurrent <= 0) return;
            // 如果 按下左鍵 就 生成子彈
            if (fire)
            {
                SpawnBullet();
                // 扣一顆子彈
                bulletCurrent--;
                StartCoroutine(BulletCD());
            }
        }

        /// <summary>
        /// 生成子彈
        /// </summary>
        protected virtual void SpawnBullet()
        {
            for (int i = 0; i < spawnBulletCount; i++)
            {
                // 生成(物件，座標，角度)
                // Quaternion.identity 零度角
                float x = Random.Range(0, spawnBulletOffsetX);
                GameObject tempBullet = Instantiate(
                    dataWeapon.bulletPrefab, 
                    spawnBulletPoint.position + Vector3.right * x, 
                    Quaternion.identity);
                // 獲得生成子彈的 2D 剛體 並添加推力 往子彈生成位置前方 (X軸) 發射
                float y = i % 2 == 0 ? i * +dataWeapon.bulletRecoil : i * -dataWeapon.bulletRecoil;
                tempBullet.GetComponent<Rigidbody2D>().AddForce(
                    spawnBulletPoint.right * dataWeapon.bulletSpeed + Vector3.up * y);
            }
        }

        private IEnumerator BulletCD()
        {
            // 不能開槍
            canFire = false;
            // 等待子彈冷卻
            yield return new WaitForSeconds(dataWeapon.bulletCD);
            // 可以開槍
            canFire = true;
        }

        protected virtual void Reload(bool reload)
        {
            // 如果 在換彈匣 就跳出
            if (isReload) return;
            // 如果 沒有 彈匣 或者 滿彈 (當前子彈等於彈匣可裝子彈數) 就 跳出
            if (magazineCount <= 0 || bulletCurrent == dataWeapon.magazineBulletCount) return;

            if (reload)
            {
                StartCoroutine(ReloadHandle());
            }
        }

        protected virtual IEnumerator ReloadHandle()
        {
            // 換彈匣中
            isReload = true;
            // 當前子彈數歸零並更新介面
            bulletCurrent = 0;
            bulletCountChange?.Invoke();
            // 等待換彈匣
            yield return new WaitForSeconds(dataWeapon.magazineCD);
            // 裝填子彈
            bulletCurrent = dataWeapon.magazineBulletCount;
            // 扣除一個彈匣並更新介面
            magazineCount--;
            bulletCountChange?.Invoke();
            // 換彈匣結束
            isReload = false;
        }
    }
}
