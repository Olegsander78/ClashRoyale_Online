using UnityEngine;

namespace ClashRoyale
{
    public class UnitAnimation : MonoBehaviour
    {
        private const string STATE = "State";
        private const string ATTACK_SPEED = "Attack_Speed";

        [SerializeField] private Animator _animator;

        public void Init(Unit unit)
        {
            var damageDelay = unit.Parameters.DamageDelay;
            _animator.SetFloat(ATTACK_SPEED, 1 / damageDelay);
        }

        public void SetState(UnitStateTypes type)
        {
            _animator.SetInteger(STATE, (int)type);
        }

    }
}
