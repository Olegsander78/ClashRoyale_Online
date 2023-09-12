using UnityEngine;

namespace ClashRoyale
{
    [CreateAssetMenu(fileName = "_NavMeshMeleeMove", menuName = "UnitStates/NavMeshMeleeMove")]
    public class NavMeshMeleeMove : UnitState_NavMeshMove
    {
        protected override bool TryFindTarget(out UnitStateTypes changedType)
        {
            if (TryAttackTower())
            {
                changedType = UnitStateTypes.ATTACK;
                return true;
            }

            if (TryChaseUnit())
            {
                changedType = UnitStateTypes.CHASE;
                return true;
            }

            changedType = UnitStateTypes.NONE;
            return false;
        }

        private bool TryAttackTower()
        {
            var distanceToTarget = NearestTower.GetDistance(Unit.transform.position);
            if (distanceToTarget <= Unit.Parameters.StartAttackDistance)
            {
                return true;
            }
            return false;
        }

        private bool TryChaseUnit()
        {
            var hasEnemy = MapInfo.Instance.TryGetNearestWalkingUnit(Unit.transform.position, TargetIsEnemy, out Unit enemy, out float distance);
            if (hasEnemy == false)
                return false;

            if (Unit.Parameters.StartChaseDistance >= distance)
            {
                return true;
            }

            return false;
        }
    }
}
