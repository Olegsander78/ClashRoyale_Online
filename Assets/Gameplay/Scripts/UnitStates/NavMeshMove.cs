using UnityEngine;
using UnityEngine.AI;

namespace UnityRoyale
{
    [CreateAssetMenu(fileName ="_NavMeshMove",menuName ="UnitStates/NavMeshMove")]
    public class NavMeshMove : UnitState
    {   
        private NavMeshAgent _agent;
        private Vector3 _targetPosition;
        private bool _targetIsEnemy;
        private Tower _nearestTower;

        public override void Constructor(Unit unit)
        {
            base.Constructor(unit);

            _targetIsEnemy = Unit.IsEnemy == false;
            _agent = Unit?.GetComponent<NavMeshAgent>();

            _agent.radius = Unit.Parameters.ModelRadius;
            _agent.speed = Unit.Parameters.Speed;
            _agent.stoppingDistance = Unit.Parameters.StartAttackDistance;
        }
        public override void Init()
        {
            var unitPosition = Unit.transform.position;
            _nearestTower = MapInfo.Instance.GetNearestTower(in unitPosition, _targetIsEnemy);
            _targetPosition = _nearestTower.transform.position;
            
            _agent.SetDestination(_targetPosition);
        }

        public override void Run()
        {
            TryAttackTower();
        }

        public override void Finish()
        {
            _agent.isStopped = true;
        }

        private void TryAttackTower()
        {
            var distanceToTarget = _nearestTower.GetDistance(Unit.transform.position); 
            if (distanceToTarget <= Unit.Parameters.StartAttackDistance)
            {
                Debug.Log("Came running");
                Unit.SetState(UnitStateTypes.ATTACK);
            }
        }
    }
}
