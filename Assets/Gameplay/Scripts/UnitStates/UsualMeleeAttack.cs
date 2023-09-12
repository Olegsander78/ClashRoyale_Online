using UnityEngine;

namespace ClashRoyale
{
    [CreateAssetMenu(fileName = "_UsualMeleeAttack", menuName = "UnitStates/UsualMeleeAttack")]
    public class UsualMeleeAttack : UnitState_Attack
    {
        protected override bool TryFindTarget(out float stopAttackDistance)
        {
            Vector3 unitPosition = Unit.transform.position;
            var hasEnemy = MapInfo.Instance.TryGetNearestWalkingUnit(unitPosition, TargetIsEnemy, out Unit enemy, out float distance);
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
    }
}
