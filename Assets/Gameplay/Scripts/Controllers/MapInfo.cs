using System;
using System.Collections.Generic;
using System.Xml.Linq;
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

        [SerializeField] private List<Unit> _enemyUnits = new List<Unit>();
        [SerializeField] private List<Unit> _playerUnits = new List<Unit>();

        private void Start()
        {
            SubscribeDestroy(_enemyTowers);
            SubscribeDestroy(_playerTowers);
            SubscribeDestroy(_enemyUnits);
            SubscribeDestroy(_playerUnits);
        }

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

        private void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}
