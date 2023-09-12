using UnityEngine;

namespace ClashRoyale
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

#if UNITY_EDITOR
        public virtual void DebugDrawDistance(Unit unit) { }
#endif
    }
}
