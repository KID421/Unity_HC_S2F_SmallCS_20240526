using TMPro;
using UnityEngine;

namespace KID
{
    /// <summary>
    /// 武器系統：玩家
    /// </summary>
    /// 子類別：父類別 (繼承)
    public class WeaponSystemPlayer : WeaponSystem
    {
        [SerializeField, Header("是否連射")]
        private bool isRepaid;
        [SerializeField, Header("介面父物件：按鈕武器")]
        private Transform uiParent;

        private TMP_Text textWeaponName;
        private TMP_Text textBulletCurrent;
        private TMP_Text textBulletTotal;
        private TMP_Text textMagazinePrice;

        // 開槍輸入按鍵，如果連射就使用 GetKey 否則使用 GetKeyDown
        private bool fireKey => isRepaid ? Input.GetKey(KeyCode.Mouse0) : Input.GetKeyDown(KeyCode.Mouse0);
        private bool reloadKey => Input.GetKeyDown(KeyCode.Mouse1);

        protected override void Awake()
        {
            base.Awake();
            // 將 玩家的更新介面方法 放到 updateUI 資料裡面
            updateUI = UpdateUI;
        }

        protected override void Update()
        {
            base.Update();
            Fire(fireKey);
            Reload(reloadKey);
#if UNITY_EDITOR
            // 如果 在編輯器內 才可以執行這邊的程式
            Test();
#endif
        }

        // 覆寫 override：覆寫父類別帶有虛擬關鍵字的成員
        protected override void Initialize()
        {
            // base 原本父類別的內容
            base.Initialize();

            // GetChild(編號) 透過編號取得子物件 0 代表父物件下面的第一個子物件
            textWeaponName = uiParent.GetChild(0).GetComponent<TMP_Text>();
            textBulletCurrent = uiParent.GetChild(1).GetComponent<TMP_Text>();
            textBulletTotal = uiParent.GetChild(2).GetComponent<TMP_Text>();
            textMagazinePrice = uiParent.GetChild(3).GetComponent<TMP_Text>();

            textWeaponName.text = dataWeapon.weaponName;
            textBulletCurrent.text = $"子彈：{dataWeapon.magazineBulletCount}";
            textBulletTotal.text = "總數：0";
            textMagazinePrice.text = $"價格：{dataWeapon.magazinePrice}";
        }

        private void UpdateUI()
        {
            textBulletCurrent.text = $"子彈：{bulletCurrent}";
            textBulletTotal.text = $"總數：{dataWeapon.magazineBulletCount * magazineCount}";
        }

        /// <summary>
        /// 測試用：添加彈匣
        /// </summary>
        private void Test()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                magazineCount++;
                UpdateUI();
            }
        }
    }
}
