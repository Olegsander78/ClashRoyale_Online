using UnityEngine;

namespace ClashRoyale
{
    [CreateAssetMenu(fileName = "_NavMeshRangeChase", menuName = "UnitStates/NavMeshRangeChase")]
    public class NavMeshRangeChase : UnitState_NavMeshChase
    {
        protected override void FindTargetUnit(out Unit targetUnit)
        {
            MapInfo.Instance.TryGetNearestAnyUnit(Unit.transform.position, TargetIsEnemy, out targetUnit, out float distance);
        }
    }
}
