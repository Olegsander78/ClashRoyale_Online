using System;
using UnityEngine;
using UnityEngine.AI;

namespace UnityRoyale
{
    [CreateAssetMenu(fileName = "_NavMeshChase", menuName = "UnitStates/NavMeshChase")]
    public class NavMeshChase : UnitState
    {   
        private NavMeshAgent _agent;
        private bool _targetIsEnemy;
        private Unit _targetUnit;

        public override void Constructor(Unit unit)
        {
            base.Constructor(unit);

            _targetIsEnemy = _unit.IsEnemy == false;
            _agent = _unit?.GetComponent<NavMeshAgent>();
        }
        public override void Init()
        {
            MapInfo.Instance.TryGetNearestUnit(_unit.transform.position, out _targetUnit, _targetIsEnemy, out float distance);
        }

        public override void Run()
        {
            if(_targetUnit == null)
            {
                _unit.SetState(UnitStateTypes.DEFAULT);
                return;
            }

            float distanceToTarget = Vector3.Distance(_unit.transform.position, _targetUnit.transform.position);
            if (distanceToTarget > _unit.Parameters.StopChaseDistance)
                _unit.SetState(UnitStateTypes.DEFAULT);
            
        }        

        public override void Finish()
        {
            
        }        
    }
}
