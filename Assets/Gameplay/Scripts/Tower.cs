using System;
using UnityEngine;
using UnityRoyale;

namespace ClashRoyale
{
    [RequireComponent(typeof(Health))]
    public class Tower : MonoBehaviour, IHealth, IDestroy
    {
        public event Action OnDestroyed;
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public float Radius { get; private set; } = 2f;

        private void Start()
        {
            Health.OnHealthChanged += CheckDestroy;
        }

        public float GetDistance(in Vector3 point)
        {
            return Vector3.Distance(transform.position, point) - Radius;
        }

        private void CheckDestroy(float currentHP)
        {
            if (currentHP > 0)
                return;

            Health.OnHealthChanged -= CheckDestroy;
            Destroy(gameObject);
            OnDestroyed?.Invoke();
        }
    }
}
