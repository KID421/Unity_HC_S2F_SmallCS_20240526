using System.Collections;
using UnityEngine;

namespace KID
{
    /// <summary>
    /// 武器系統：敵人
    /// </summary>
    public class WeaponSystenEnemy : WeaponSystem
    {
        [Header("敵人攻擊間隔")]
        [SerializeField, Range(0, 10)]
        private float attackIntervalMin = 3;
        [SerializeField, Range(0, 10)]
        private float attackIntervalMax = 10;

        private float attackInterval;
        private WaitForSeconds waitAttackInterval;
        private bool enemyAttack;
        private ControlSystemEnemy controlSystemEnemy;

        protected override void Awake()
        {
            base.Awake();
            controlSystemEnemy = transform.root.GetComponent<ControlSystemEnemy>();
            attackInterval = Random.Range(attackIntervalMin, attackIntervalMax);
            waitAttackInterval = new WaitForSeconds(attackInterval);
        }

        protected override void Update()
        {
            base.Update();

            if (enemyAttack) return;
            StartCoroutine(EnemyFire());

            Fire(controlSystemEnemy.checkPlayer);
        }

        private IEnumerator EnemyFire()
        {
            enemyAttack = true;
            yield return waitAttackInterval;
            enemyAttack = false;
        }
    }
}
