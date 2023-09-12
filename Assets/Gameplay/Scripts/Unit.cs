using System;
using UnityEngine;
using UnityRoyale;

namespace ClashRoyale
{
    [RequireComponent(typeof(UnitParameters), typeof(Health))]
    public class Unit : MonoBehaviour, IHealth, IDestroy
    {
        public event Action OnDestroyed;
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public bool IsEnemy { get; private set; } = false;
        [field: SerializeField] public UnitParameters Parameters;

        [SerializeField] private UnitState _defaultStateSO;
        [SerializeField] private UnitState _chaseStateSO;
        [SerializeField] private UnitState _attackStateSO;

        private UnitState _defaultState;
        private UnitState _chaseState;
        private UnitState _attackState;
        private UnitState _currentState;

        private void Start()
        {
            CreateStates();

            _currentState = _defaultState;
            _currentState.Init();

            Health.OnHealthChanged += CheckDestroy;
        }        

        private void Update()
        {
            _currentState.Run();
        }

        public void SetState(UnitStateTypes type)
        {
            _currentState.Finish();

            switch (type)
            {                
                case UnitStateTypes.DEFAULT:
                    _currentState = _defaultState;
                    break;
                case UnitStateTypes.CHASE:
                    _currentState = _chaseState;
                    break;
                case UnitStateTypes.ATTACK:
                    _currentState = _attackState;
                    break;
                default:
                    Debug.LogError("State not handled" + type);
                    break;
            }

            _currentState.Init();
        }

        private void CreateStates()
        {
            _defaultState = Instantiate(_defaultStateSO);
            _defaultState.Constructor(this);
            _chaseState = Instantiate(_chaseStateSO);
            _chaseState.Constructor(this);
            _attackState = Instantiate(_attackStateSO);
            _attackState.Constructor(this);
        }

        private void CheckDestroy(float currentHP)
        {
            if (currentHP > 0)
                return;

            Health.OnHealthChanged -= CheckDestroy;
            Destroy(gameObject);
            OnDestroyed?.Invoke();
        }

#if UNITY_EDITOR
        [SerializeField] private bool _debugOn = false;        

        private void OnDrawGizmos()
        {
            if (_debugOn == false)
                return;

            if (_chaseStateSO != null)
                _chaseStateSO.DebugDrawDistance(this);
        }
#endif
    }
}
