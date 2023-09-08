using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class MapInfo : MonoBehaviour
    {
        #region SingletonOneScene
        public static MapInfo Instance { get; private set; }
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
        #endregion

        [SerializeField] private List<Transform> _enemyTowers = new List<Transform>();
        [SerializeField] private List<Transform> _playerTowers = new List<Transform>();

        public Vector3 GetNearestTowerPosition(in Vector3 currentPosition, bool isEnemy)
        {
            var towers = isEnemy ? _enemyTowers : _playerTowers;
            var nearestTowerPosition = towers[0].position;
            var distance = Vector3.Distance(currentPosition, towers[0].position);

            for (int i = 1; i < towers.Count; i++)
            {
                var tempDistance = Vector3.Distance(currentPosition, towers[i].position);
                if (tempDistance > distance)
                    continue;

                nearestTowerPosition = towers[i].position;
                distance = tempDistance;
            }

            return nearestTowerPosition;
        }
    }
}
