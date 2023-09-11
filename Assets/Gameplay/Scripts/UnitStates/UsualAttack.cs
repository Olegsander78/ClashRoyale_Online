using UnityEngine;

namespace UnityRoyale
{
    [CreateAssetMenu(fileName = "_UsualAttack", menuName = "UnitStates/UsualAttack")]
    public class UsualAttack : UnitState
    {
        [SerializeField] private float _damage = 1.5f;
        [SerializeField] private float _delay = 1f;

        private float _time = 0f;
        private float _stopAttackDistance = 0;
        private bool _targetIsEnemy;
        private Health _target;

        public override void Constructor(Unit unit)
        {
            base.Constructor(unit);

            _targetIsEnemy = _unit.IsEnemy == false;
        }
        public override void Init()
        {
            if (TryFindTarget(out _stopAttackDistance) == false)
            {
                _unit.SetState(UnitStateTypes.DEFAULT);
                return;
            }

            _time = 0f;
            _unit.transform.LookAt(_target.transform.position);
        }

        public override void Run()
        {
            _time += Time.deltaTime;
            if (_time < _delay)
                return;
            _time -= _delay;

            if (_target == false)
            {
                _unit.SetState(UnitStateTypes.DEFAULT);
                return;
            }

            var distanceToTarget = Vector3.Distance(_target.transform.position, _unit.transform.position);
            if (distanceToTarget > _stopAttackDistance)
                _unit.SetState(UnitStateTypes.CHASE);
            _target.ApplyDamage(_damage);
        }

        public override void Finish()
        {
            
        }

        private bool TryFindTarget(out float stopAttackDistance)
        {
            Vector3 unitPosition = _unit.transform.position;
            var hasEnemy = MapInfo.Instance.TryGetNearestUnit(unitPosition, _targetIsEnemy, out Unit enemy, out float distance);
            if (hasEnemy && distance - enemy.Parameters.ModelRadius <= _unit.Parameters.StartAttackDistance)
            {
                _target = enemy.Health;
                stopAttackDistance = _unit.Parameters.StopAttackDistance + enemy.Parameters.ModelRadius;
                return true;
            }

            Tower targetTower = MapInfo.Instance.GetNearestTower(unitPosition, _targetIsEnemy);
            if (targetTower.GetDistance(unitPosition) <= _unit.Parameters.StartAttackDistance)
            {
                _target = targetTower.Health;
                stopAttackDistance = _unit.Parameters.StopAttackDistance + targetTower.Radius;
                return true;
            }

            stopAttackDistance = 0f;
            return false;
        }
    }
}
