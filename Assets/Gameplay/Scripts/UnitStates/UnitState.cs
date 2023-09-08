using UnityEngine;

namespace UnityRoyale
{
    public abstract class UnitState : ScriptableObject
    {
        protected Unit _unit;
        public virtual void Constructor(Unit unit)
        {
            _unit = unit;
        }
        public abstract void Init();
        public abstract void Finish();
        public abstract void Run();
    }
}
