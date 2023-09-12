using UnityEngine;

namespace ClashRoyale
{
    public abstract class UnitState_Attack : UnitState
    {
        [SerializeField] private float _damage = 1.5f;
        [SerializeField] private float _delay = 1f;

        protected bool TargetIsEnemy;
        protected Health Target;

        private float _time = 0f;
        private float _stopAttackDistance = 0;

        public override void Constructor(Unit unit)
        {
            base.Constructor(unit);

            TargetIsEnemy = Unit.IsEnemy == false;
        }
        public override void Init()
        {
            if (TryFindTarget(out _stopAttackDistance) == false)
            {
                Unit.SetState(UnitStateTypes.DEFAULT);
                return;
            }

            _time = 0f;
            Unit.transform.LookAt(Target.transform.position);
        }

        public override void Run()
        {
            _time += Time.deltaTime;
            if (_time < _delay)
                return;
            _time -= _delay;

            if (Target == false)
            {
                Unit.SetState(UnitStateTypes.DEFAULT);
                return;
            }

            var distanceToTarget = Vector3.Distance(Target.transform.position, Unit.transform.position);
            if (distanceToTarget > _stopAttackDistance)
                Unit.SetState(UnitStateTypes.CHASE);
            Target.ApplyDamage(_damage);
        }

        public override void Finish()
        {
        }

        protected abstract bool TryFindTarget(out float stopAttackDistance);
    }
}
