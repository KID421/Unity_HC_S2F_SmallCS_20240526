using TMPro;
using UnityEngine;

namespace KID
{
    /// <summary>
    /// 武器系統：玩家
    /// </summary>
    public class WeaponSystemPlayer : WeaponSystem
    {
        [SerializeField, Header("介面父物件")]
        private Transform uiParent;
        [SerializeField, Header("是否連射")]
        private bool isRapid;
        [SerializeField, Header("是否無限彈匣")]
        private bool unlimitedMagazine;

        private string nameWeaponName = "文字武器名稱";
        private string nameBulletCurrent = "文字當前子彈";
        private string nameBulletTotal = "文字子彈總數";
        private string nameMagazinePrice = "文字彈匣價格";
        protected TMP_Text textWeaponName;
        protected TMP_Text textBulletCurrent;
        protected TMP_Text textBulletTotal;
        protected TMP_Text textMagazinePrice;

        protected virtual bool fireKey => isRapid ? Input.GetKey(KeyCode.Mouse0) : Input.GetKeyDown(KeyCode.Mouse0);
        private string stringBulletTotal => $"總數：{(unlimitedMagazine ? "∞" : "0")}";

        protected override void Awake()
        {
            textWeaponName = uiParent.Find(nameWeaponName).GetComponent<TMP_Text>();
            textBulletCurrent = uiParent.Find(nameBulletCurrent).GetComponent<TMP_Text>();
            textBulletTotal = uiParent.Find(nameBulletTotal).GetComponent<TMP_Text>();
            textMagazinePrice = uiParent.Find(nameMagazinePrice).GetComponent<TMP_Text>();
            base.Awake();
            bulletCountChange = UpdateUI;
        }

        private void Update()
        {
            Fire(fireKey);
            Reload(Input.GetKeyDown(KeyCode.Mouse1));
        }

        protected override void Initialize()
        {
            base.Initialize();
            textWeaponName.text = dataWeapon.weaponName;
            textBulletCurrent.text = $"子彈：{dataWeapon.magazineBulletCount}";
            textBulletTotal.text = stringBulletTotal;
            textMagazinePrice.text = $"價格：{dataWeapon.magazinePrice}";
            if (unlimitedMagazine) magazineCount = 999;
        }

        protected override void Fire(bool fire)
        {
            base.Fire(fire);
            UpdateUI();
        }

        protected virtual void UpdateUI()
        {
            textBulletCurrent.text = $"子彈：{bulletCurrent}";
            textBulletTotal.text = stringBulletTotal;
        }

        protected override void Reload(bool reload)
        {
            base.Reload(reload);
            if (unlimitedMagazine) magazineCount = 999;
        }
    }
}
