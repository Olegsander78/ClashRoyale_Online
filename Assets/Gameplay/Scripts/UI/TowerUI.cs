using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityRoyale
{
    public class TowerUI : MonoBehaviour
    {
        [SerializeField] private Tower _tower;
        [SerializeField] private GameObject _healthBar;
        [SerializeField] private Image _fillHealthImage;

        private float _maxHealth;

        private void Start()
        {
            _healthBar.SetActive(false);
            _maxHealth = _tower.Health.Max;

            _tower.Health.OnHealthChanged += UpdateHealth;
        }

        private void OnDestroy()
        {
            _tower.Health.OnHealthChanged -= UpdateHealth;
        }

        private void UpdateHealth(float currentValue)
        {
            _healthBar.SetActive(true);
            _fillHealthImage.fillAmount = currentValue / _maxHealth;
        }
    }
}