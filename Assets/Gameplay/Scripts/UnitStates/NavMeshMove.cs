using UnityEngine;
using UnityEngine.AI;

namespace UnityRoyale
{
    [CreateAssetMenu(fileName ="_NavMeshMove",menuName ="UnitStates/NavMeshMove")]
    public class NavMeshMove : UnitState
    {
        [SerializeField] private bool _isEnemy = false;
        [SerializeField] private float _moveOffset = 1f;
        private NavMeshAgent _agent;
        private Vector3 _targetPosition;
                
        public override void Init()
        {
            var unitPosition = Unit.transform.position;
            _targetPosition = MapInfo.Instance.GetNearestTowerPosition(in unitPosition,_isEnemy == false);

            _agent = Unit.GetComponent<NavMeshAgent>();
            _agent.SetDestination(_targetPosition);
        }

        public override void Run()
        {
            var distanceToTarget = Vector3.Distance(Unit.transform.position, _targetPosition);
            if(distanceToTarget <= _moveOffset)
            {
                Debug.Log("Came running");
                Unit.SetState(UnitStateTypes.ATTACK);
            }
        }

        public override void Finish()
        {
            _agent.isStopped = true;
        }
    }
}
