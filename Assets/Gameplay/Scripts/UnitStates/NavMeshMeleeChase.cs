using UnityEngine;

namespace ClashRoyale
{
    [CreateAssetMenu(fileName = "_NavMeshMeleeChase", menuName = "UnitStates/NavMeshMeleeChase")]
    public class NavMeshMeleeChase : UnitState_NavMeshChase
    {
        protected override void FindTargetUnit(out Unit targetUnit)
        {
            MapInfo.Instance.TryGetNearestWalkingUnit(Unit.transform.position, TargetIsEnemy, out targetUnit, out float distance);
        }
    }
}
