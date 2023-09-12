using UnityEngine.AI;

namespace ClashRoyale
{
    public abstract class UnitState_NavMeshMove : UnitState
    {
        protected Tower NearestTower;
        protected bool TargetIsEnemy;

        private NavMeshAgent _agent;

        public override void Constructor(Unit unit)
        {
            base.Constructor(unit);

            TargetIsEnemy = Unit.IsEnemy == false;
            _agent = Unit?.GetComponent<NavMeshAgent>();

            _agent.radius = Unit.Parameters.ModelRadius;
            _agent.speed = Unit.Parameters.Speed;
            _agent.stoppingDistance = Unit.Parameters.StartAttackDistance;
        }
        public override void Init()
        {
            var unitPosition = Unit.transform.position;
            NearestTower = MapInfo.Instance.GetNearestTower(in unitPosition, TargetIsEnemy);

            _agent.SetDestination(NearestTower.transform.position);
        }

        public override void Run()
        {
            if (TryFindTarget(out UnitStateTypes changedType))
                Unit.SetState(changedType);
        }

        public override void Finish()
        {
            _agent.SetDestination(Unit.transform.position);
        }

        protected abstract bool TryFindTarget(out UnitStateTypes changedType);
    }
}
