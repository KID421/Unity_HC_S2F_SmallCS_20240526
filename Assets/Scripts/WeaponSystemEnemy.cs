using System.Collections;
using UnityEngine;

namespace KID
{
    /// <summary>
    /// 武器系統：敵人
    /// </summary>
    public class WeaponSystemEnemy : WeaponSystem
    {
        [Header("開槍間隔")]
        [SerializeField, Range(0, 2)]
        private float fireIntervalMin = 2;
        [SerializeField, Range(2, 5)]
        private float fireIntervalMax = 4;

        private ControlSystemEnemy controlSystemEnemy;
        private bool enemyFire;

        protected override void Awake()
        {
            base.Awake();
            controlSystemEnemy = transform.root.GetComponent<ControlSystemEnemy>();
        }

        protected override void Update()
        {
            base.Update();
            EnemyInput();
        }

        private void EnemyInput()
        {
            // 如果在開槍就跳出
            if (enemyFire) return;
            Fire(controlSystemEnemy.checkPlayer);
            // 啟動開槍間隔協同程序
            StartCoroutine(FireInterval());
        }

        private IEnumerator FireInterval()
        {
            // 正在開槍中
            enemyFire = true;
            // 獲得隨機開槍間隔並等待
            float fireInterval = Random.Range(fireIntervalMin, fireIntervalMax);
            yield return new WaitForSeconds(fireInterval);
            // 恢復為沒有開槍
            enemyFire = false;
        }
    }
}
