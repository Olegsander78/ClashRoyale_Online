using System.Collections.Generic;
using UnityEngine;
using UnityRoyale;

namespace ClashRoyale
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

        [SerializeField] private List<Unit> _enemyWalkingUnits = new List<Unit>();
        [SerializeField] private List<Unit> _playerWalkingUnits = new List<Unit>();
        [SerializeField] private List<Unit> _enemyFlyingUnits = new List<Unit>();
        [SerializeField] private List<Unit> _playerFlyingUnits = new List<Unit>();

        private void Start()
        {
            SubscribeDestroy(_enemyTowers);
            SubscribeDestroy(_playerTowers);
            SubscribeDestroy(_enemyWalkingUnits);
            SubscribeDestroy(_playerWalkingUnits);
        }

        public void AddUnit(Unit unit)
        {
            List<Unit> list;

            if (unit.IsEnemy == true)
                list = unit.Parameters.IsFlying ? _enemyFlyingUnits : _enemyWalkingUnits;
            else
                list = unit.Parameters.IsFlying ? _playerFlyingUnits : _playerWalkingUnits;

            AddObjectToList(list, unit);
        }

        public bool TryGetNearestAnyUnit(in Vector3 currentPosition, bool isEnemy, out Unit unit,  out float distance)
        {
            TryGetNearestWalkingUnit(currentPosition, isEnemy, out Unit walking, out float walkingDistance);
            TryGetNearestFlyingUnit(currentPosition, isEnemy, out Unit flying, out float flyingDistance);

            if(flyingDistance < walkingDistance)
            {
                unit = flying;
                distance = flyingDistance;
            }
            else
            {
                unit = walking;
                distance = walkingDistance;
            }

            return unit;
        }

        public bool TryGetNearestWalkingUnit(in Vector3 currentPosition, bool isEnemy, out Unit unit, out float distance)
        {
            var units = isEnemy ? _enemyWalkingUnits : _playerWalkingUnits;
            unit = GetNearestObject(currentPosition, units, out distance);
            return unit;
        }

        public bool TryGetNearestFlyingUnit(in Vector3 currentPosition, bool isEnemy, out Unit unit, out float distance)
        {
            var units = isEnemy ? _enemyFlyingUnits : _playerFlyingUnits;
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

        private void SubscribeDestroy<T>(List<T> objects) where T: IDestroy
        {
            for (int i = 0; i < objects.Count; i++)
            {
                var obj = objects[i];
                objects[i].OnDestroyed += RemoveAndUnsubscribe;

                void RemoveAndUnsubscribe()
                {
                    RemoveObjectFromList(objects, obj);
                    obj.OnDestroyed -= RemoveAndUnsubscribe;
                }
            }
        }

        private void AddObjectToList<T>(List<T> list, T obj) where T: IDestroy
        {
            list.Add(obj);
            obj.OnDestroyed += RemoveAndUnsubscribe;

            void RemoveAndUnsubscribe()
            {
                RemoveObjectFromList(list, obj);
                obj.OnDestroyed -= RemoveAndUnsubscribe;
            }
        }
        private void RemoveObjectFromList<T>(List<T> list, T obj)
        {
            if (list.Contains(obj))
                list.Remove(obj);
        }
    }
}
