using UnityEngine;

namespace ClashRoyale
{
    [CreateAssetMenu(fileName = "_UsualRangeAttack", menuName = "UnitStates/UsualRangeAttack")]
    public class UsualRangeAttack : UnitState_Attack
    {
        [SerializeField] private Arrow _arrow;
        protected override  bool TryFindTarget(out float stopAttackDistance)
        {
            Vector3 unitPosition = Unit.transform.position;
            var hasEnemy = MapInfo.Instance.TryGetNearestAnyUnit(unitPosition, TargetIsEnemy, out Unit enemy, out float distance);
            if (hasEnemy && distance - enemy.Parameters.ModelRadius <= Unit.Parameters.StartAttackDistance)
            {
                Target = enemy.Health;
                stopAttackDistance = Unit.Parameters.StopAttackDistance + enemy.Parameters.ModelRadius;
                return true;
            }

            Tower targetTower = MapInfo.Instance.GetNearestTower(unitPosition, TargetIsEnemy);
            if (targetTower.GetDistance(unitPosition) <= Unit.Parameters.StartAttackDistance)
            {
                Target = targetTower.Health;
                stopAttackDistance = Unit.Parameters.StopAttackDistance + targetTower.Radius;
                return true;
            }

            stopAttackDistance = 0f;
            return false;
        }

        protected override void Attack()
        {
            var unitPosition = Unit.transform.position;
            var targetPosition = Target.transform.position;

            Arrow arrow = Instantiate(_arrow, unitPosition, Quaternion.identity);
            arrow.Init(targetPosition);
            var delay = Vector3.Distance(unitPosition,targetPosition) / arrow.Speed;
            Target.ApplyDelayDamage(delay, Damage);
        }
    }
}
