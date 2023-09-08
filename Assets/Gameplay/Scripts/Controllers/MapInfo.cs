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

        [SerializeField] private List<Tower> _enemyTowers = new List<Tower>();
        [SerializeField] private List<Tower> _playerTowers = new List<Tower>();

        public Tower GetNearestTower(in Vector3 currentPosition, bool isEnemy)
        {
            var towers = isEnemy ? _enemyTowers : _playerTowers;
            var nearestTower = towers[0];
            var distance = Vector3.Distance(currentPosition, towers[0].transform.position);

            for (int i = 1; i < towers.Count; i++)
            {
                var tempDistance = Vector3.Distance(currentPosition, towers[i].transform.position);
                if (tempDistance > distance)
                    continue;

                nearestTower = towers[i];
                distance = tempDistance;
            }

            return nearestTower;
        }
    }
}
