using System;
using TMPro;
using UnityEngine;

namespace KID
{
    /// <summary>
    /// 遊戲管理器
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // 公開給外部存取的變數
        public static GameManager instance
        {
            // 唯讀(其他腳本只能取得此資料)
            get
            {
                // 如果實體為空的 就尋找場景上有 GM 的物件並存放到實體
                if (_instance == null) _instance = FindObjectOfType<GameManager>();
                // 傳回實體
                return _instance;
            }
        }

        // 用來存放單例的變數(實體)
        private static GameManager _instance;

        // 定義事件，並且攜帶一個參數 DataWeapon，事件習慣用 on 開頭命名
        // 購買彈匣事件，攜帶當前購買的武器資料
        public event EventHandler<DataWeapon> onBuyMagazine;

        // const 常數：不會變的值
        // 存取方式：腳本名稱.常數名稱
        public const string playerName = "玩家_伊莉莎白";

        [SerializeField, Header("武器資料")]
        private DataWeapon[] dataWeapons;

        private TMP_Text textKillCount, textCoin;
        private int killCount, coin;
        private int coinIncrease = 100;

        private void Awake()
        {
            textKillCount = GameObject.Find("文字擊殺數量").GetComponent<TMP_Text>();
            textCoin = GameObject.Find("文字金幣數量").GetComponent<TMP_Text>();
        }

        private void Update()
        {
            BuyMagazine();
        }

        /// <summary>
        /// 更新擊殺數與金幣數值跟介面
        /// </summary>
        public void UpdateKillAndCoin()
        {
            killCount++;
            coin += coinIncrease;
            textKillCount.text = $"擊殺數量：{killCount}";
            textCoin.text = $"金幣：{coin}";
        }

        private void BuyMagazine()
        {
            // 迴圈重複執行所有可買彈匣的武器
            for (int i = 0; i < dataWeapons.Length; i++)
            {
                // 獲得每一個武器的資料
                var weapon = dataWeapons[i];
                // 如果玩家按下該武器的購買按鈕
                if (Input.GetKeyDown(weapon.buyMagazineKey))
                {
                    // 如果 錢 小於 武器的彈匣價格 就 跳出
                    if (coin < weapon.magazinePrice) return;
                    // 扣錢與更新介面
                    coin -= weapon.magazinePrice;
                    textCoin.text = $"金幣：{coin}";
                    // 呼叫事件
                    // ?.Invoke 有人訂閱此事件 才會進行呼叫
                    // (呼叫事件的物件，攜帶的參數)
                    // 呼叫購買彈匣事件，並將此物件以及購買的武器資料傳遞出去
                    onBuyMagazine?.Invoke(this, weapon);
                    SoundManager.instance.PlaySound(SoundType.Buy);
                }
            }
        }
    }
}
