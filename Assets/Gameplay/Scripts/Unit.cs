using UnityEngine;

namespace UnityRoyale
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitState _defaultStateSO;
        [SerializeField] private UnitState _chaseStateSO;
        [SerializeField] private UnitState _attackStateSO;

        private UnitState _defaultState;
        private UnitState _chaseState;
        private UnitState _attackState;
        private UnitState _currentState;

        private void Start()
        {
            _defaultState = Instantiate(_defaultStateSO);
            _defaultState.Constructor(this);
            _chaseState = Instantiate(_chaseStateSO);
            _chaseState.Constructor(this);
            _attackState = Instantiate(_attackStateSO);
            _attackState.Constructor(this);

            _currentState = _defaultState;
            _currentState.Init();
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
    }
}
