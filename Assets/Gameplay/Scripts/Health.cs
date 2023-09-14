using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClashRoyale
{
    public class Health : MonoBehaviour
    {
        public event Action<float> OnHealthChanged;
        [field: SerializeField] public float Max { get; private set; } = 10f;

        private float _current;

        private void Start()
        {
            _current = Max;
        }

        public void ApplyDamage(float value)
        {
            _current -= value;
            if (_current <= 0)
                _current = 0f;

            OnHealthChanged?.Invoke(_current);
        }

        public void ApplyDelayDamage(float delay, float damage)
        {
            StartCoroutine(DelayDamageRoutine(delay, damage));
        }

        private IEnumerator DelayDamageRoutine(float delay, float damage)
        {
            yield return new WaitForSeconds(delay);
            ApplyDamage(damage);
        }
    }
}
