using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace ClashRoyale
{
    public abstract class UnitState_NavMeshChase : UnitState
    {
        protected bool TargetIsEnemy;
        protected Unit TargetUnit;
        protected float StartAttackDistance = 0;

        private NavMeshAgent _agent;

        public override void Constructor(Unit unit)
        {
            base.Constructor(unit);

            TargetIsEnemy = Unit.IsEnemy == false;
            _agent = Unit?.GetComponent<NavMeshAgent>();
        }
        public override void Init()
        {
            FindTargetUnit(out TargetUnit);

            if (TargetUnit == null)
            {
                Unit.SetState(UnitStateTypes.DEFAULT);
                return;
            }

            StartAttackDistance = Unit.Parameters.StartAttackDistance + TargetUnit.Parameters.ModelRadius;
        }

        public override void Run()
        {
            if (TargetUnit == null)
            {
                Unit.SetState(UnitStateTypes.DEFAULT);
                return;
            }

            float distanceToTarget = Vector3.Distance(Unit.transform.position, TargetUnit.transform.position);
            if (distanceToTarget > Unit.Parameters.StopChaseDistance)
                Unit.SetState(UnitStateTypes.DEFAULT);
            else if (distanceToTarget <= Unit.Parameters.StartAttackDistance + TargetUnit.Parameters.ModelRadius)
                Unit.SetState(UnitStateTypes.ATTACK);
            else _agent.SetDestination(TargetUnit.transform.position);

        }

        public override void Finish()
        {
            _agent.SetDestination(Unit.transform.position);
        }

#if UNITY_EDITOR
        public override void DebugDrawDistance(Unit unit)
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.Parameters.StartChaseDistance);
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.Parameters.StopChaseDistance);
        }
#endif

        protected abstract void FindTargetUnit(out Unit targetUnit);
    }
}
