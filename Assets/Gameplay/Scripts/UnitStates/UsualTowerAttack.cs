using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClashRoyale
{
    [CreateAssetMenu(fileName = "_UsualTowerAttack", menuName = "UnitStates/UsualTowerAttack")]
    public class UsualTowerAttack : UnitState_Attack
    {
        protected override bool TryFindTarget(out float stopAttackDistance)
        {
            Vector3 unitPosition = Unit.transform.position;
            
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
