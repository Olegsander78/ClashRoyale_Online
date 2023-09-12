using UnityEngine;

namespace ClashRoyale
{
    [CreateAssetMenu(fileName = "_NavMeshTowerMove", menuName = "UnitStates/NavMeshTowerMove")]
    public class NavMeshTowerMove : UnitState_NavMeshMove
    {
        protected override bool TryFindTarget(out UnitStateTypes changedType)
        {
            var distanceToTarget = NearestTower.GetDistance(Unit.transform.position);

            if (distanceToTarget <= Unit.Parameters.StartAttackDistance)
            {
                changedType = UnitStateTypes.ATTACK;
                return true;
            }
            else
            {
                changedType = UnitStateTypes.NONE;
                return false;
            }
        }
    }
}
