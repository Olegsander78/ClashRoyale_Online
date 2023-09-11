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

        [SerializeField] private List<Unit> _enemyUnits = new List<Unit>();
        [SerializeField] private List<Unit> _playerUnits = new List<Unit>();

        public bool TryGetNearestUnit(in Vector3 currentPosition, bool isEnemy, out Unit unit,  out float distance)
        {
            var units = isEnemy ? _enemyUnits : _playerUnits;
            unit = GetNearestObject(currentPosition, units, out distance);
            return unit;
        }

        public Tower GetNearestTower(in Vector3 currentPosition, bool isEnemy)
        {
            var towers = isEnemy ? _enemyTowers : _playerTowers;
            return GetNearestObject(currentPosition, towers, out float distance);
        }

        private T GetNearestObject<T>(in Vector3 currentPosition, List<T> objects, out float distance) where T: MonoBehaviour
        {
            distance = float.MaxValue;
            if (objects.Count <= 0)
                return null;

            distance = Vector3.Distance(currentPosition, objects[0].transform.position);
            T nearestObject = objects[0];

            for (int i = 0; i < objects.Count; i++)
            {
                var tempDistance = Vector3.Distance(currentPosition, objects[i].transform.position);
                if (tempDistance > distance)
                    continue;

                nearestObject = objects[i];
                distance = tempDistance;
            }

            return nearestObject;
        }
    }
}
