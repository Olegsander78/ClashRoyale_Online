using UnityEngine;

namespace UnityRoyale
{
    public class UnitParameters : MonoBehaviour
    {        
        [SerializeField] private float _speed = 4f;
        [SerializeField] private float _startAttackDistance = 1f;
        [SerializeField] private float _stopAttackDistance = 1.5f;
        [SerializeField] private float _modelRadius;

        public float Speed { get => _speed; private set => _speed = value; }
        public float StartAttackDistance { get => _startAttackDistance + _modelRadius; private set => _startAttackDistance = value; }
        public float StopAttackDistance { get => _stopAttackDistance + _modelRadius; set => _stopAttackDistance = value; }
        public float ModelRadius { get => _modelRadius; private set => _modelRadius = value; }
    }
}
