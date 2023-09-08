using UnityEngine;

namespace UnityRoyale
{
    public abstract class UnitState : ScriptableObject
    {
        protected Unit Unit;

        public virtual void Constructor(Unit unit)
        {
            Unit = unit;
        }

        public abstract void Init();
        public abstract void Finish();
        public abstract void Run();
    }
}
