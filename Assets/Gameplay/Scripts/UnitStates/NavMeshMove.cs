using UnityEngine;
using UnityEngine.AI;

namespace UnityRoyale
{
    [CreateAssetMenu(fileName ="_NavMeshMove",menuName ="UnitStates/NavMeshMove")]
    public class NavMeshMove : UnitState
    {   
        private NavMeshAgent _agent;
        private bool _targetIsEnemy;
        private Tower _nearestTower;

        public override void Constructor(Unit unit)
        {
            base.Constructor(unit);

            _targetIsEnemy = _unit.IsEnemy == false;
            _agent = _unit?.GetComponent<NavMeshAgent>();

            _agent.radius = _unit.Parameters.ModelRadius;
            _agent.speed = _unit.Parameters.Speed;
            _agent.stoppingDistance = _unit.Parameters.StartAttackDistance;
        }
        public override void Init()
        {
            var unitPosition = _unit.transform.position;
            _nearestTower = MapInfo.Instance.GetNearestTower(in unitPosition, _targetIsEnemy);
            
            _agent.SetDestination(_nearestTower.transform.position);
        }

        public override void Run()
        {
            if (TryAttackTower())
                return;
            if (TryAttackUnit())
                return;
        }        

        public override void Finish()
        {
            _agent.SetDestination(_unit.transform.position);
        }

        private bool TryAttackTower()
        {
            var distanceToTarget = _nearestTower.GetDistance(_unit.transform.position); 
            if (distanceToTarget <= _unit.Parameters.StartAttackDistance)
            {
                _unit.SetState(UnitStateTypes.ATTACK);
                return true;
            }
            return false;
        }

        private bool TryAttackUnit()
        {
            var hasEnemy = MapInfo.Instance.TryGetNearestUnit(_unit.transform.position, _targetIsEnemy, out Unit enemy,  out float distance);
            if (hasEnemy == false)
                return false;

            if(_unit.Parameters.StartChaseDistance >= distance)
            {
                _unit.SetState(UnitStateTypes.CHASE);
                return true;
            }

            return false;
        }
    }
}
