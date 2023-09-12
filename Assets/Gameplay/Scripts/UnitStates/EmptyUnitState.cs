using UnityEngine;

namespace ClashRoyale
{
    [CreateAssetMenu(fileName = "EmptyState", menuName = "UnitStates/EmptyState")]
    public class EmptyUnitState : UnitState
    {
        public override void Init()
        {
            Unit.SetState(UnitStateTypes.DEFAULT);
        }

        public override void Run()
        {
            
        }

        public override void Finish()
        {
            Debug.LogWarning($"Unit {Unit.name} was empty state.");
        }

    }
}
