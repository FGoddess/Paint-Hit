using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool Instance => _instance;

    private Dictionary<PoolType, List<GameObject>> _pooledObjectsDictionary;

    [SerializeField] private List<ObjectToPool> _objectsToPool;

    [SerializeField] private Transform _container;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        _pooledObjectsDictionary = new Dictionary<PoolType, List<GameObject>>();
        
        for (int i = 0; i < _objectsToPool.Count; i++)
        {
            if (!_pooledObjectsDictionary.ContainsKey(_objectsToPool[i].PoolType))
            {
                _objectsToPool[i].PooledObjects = new List<GameObject>();
                var container = Instantiate(_container);
                container.name = _objectsToPool[i].PoolType.ToString() + "Container";

                for (int j = 0; j < _objectsToPool[i].AmountToPool; j++)
                {
                    var temp = Instantiate(_objectsToPool[i].ObjectPrefab, container.transform);
                    temp.SetActive(false);
                    _objectsToPool[i].PooledObjects.Add(temp);
                }

                _pooledObjectsDictionary.Add(_objectsToPool[i].PoolType, _objectsToPool[i].PooledObjects);
            }
            else
            {
                Debug.LogWarning("Pooltype already exist!!!");

                if (_pooledObjectsDictionary.TryGetValue(_objectsToPool[i].PoolType, out List<GameObject> pool))
                {
                    for (int j = 0; j < _objectsToPool[i].AmountToPool; j++)
                    {
                        var temp = Instantiate(_objectsToPool[i].ObjectPrefab, pool[0].transform.parent);
                        temp.SetActive(false);
                        pool.Add(temp);
                    }
                }
            }
        }
    }

    public GameObject GetPooledObject(PoolType poolType)
    {
        if(_pooledObjectsDictionary.TryGetValue(poolType, out List<GameObject> pool))
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    return pool[i];
                }
            }
        }

        return null;
    }
}