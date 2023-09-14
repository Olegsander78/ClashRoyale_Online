using UnityEngine;

namespace ClashRoyale
{
    public abstract class UnitState_Attack : UnitState
    {
        [SerializeField] protected float Damage = 1.5f;
        [SerializeField] private float _delay = 1f;

        protected bool TargetIsEnemy;
        protected Health Target;

        private float _time = 0f;
        private float _stopAttackDistance = 0;

        public override void Constructor(Unit unit)
        {
            base.Constructor(unit);

            TargetIsEnemy = Unit.IsEnemy == false;
            _delay = Unit.Parameters.DamageDelay;
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
            if (Target == false)
            {
                Unit.SetState(UnitStateTypes.DEFAULT);
                return;
            }

            _time += Time.deltaTime;
            if (_time < _delay)
                return;
            _time -= _delay;

            var distanceToTarget = Vector3.Distance(Target.transform.position, Unit.transform.position);
            if (distanceToTarget > _stopAttackDistance)
                Unit.SetState(UnitStateTypes.CHASE);

            Attack();
        }

        public override void Finish()
        {
        }

        protected virtual void Attack()
        {
            Target.ApplyDamage(Damage);
        }

        protected abstract bool TryFindTarget(out float stopAttackDistance);
    }
}
