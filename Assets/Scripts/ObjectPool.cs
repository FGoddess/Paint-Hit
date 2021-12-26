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
            var container = Instantiate(_container);

            _objectsToPool[i].PooledObjects = new List<GameObject>();

            for (int j = 0; j < _objectsToPool[i].AmountToPool; j++)
            {
                var temp = Instantiate(_objectsToPool[i].ObjectPrefab, container.transform);
                temp.SetActive(false);
                _objectsToPool[i].PooledObjects.Add(temp);
            }

            _pooledObjectsDictionary.Add(_objectsToPool[i].PoolType, _objectsToPool[i].PooledObjects);
        }
    }

    public GameObject GetPooledObject(PoolType poolType)
    {
        _pooledObjectsDictionary.TryGetValue(poolType, out List<GameObject> pool);

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        } 

        return null;
    }
}

[Serializable]
public class ObjectToPool
{
    public PoolType PoolType;
    public GameObject ObjectPrefab;
    public int AmountToPool;
    public List<GameObject> PooledObjects;
}

public enum PoolType
{
    Ball,
    Circle,
    BallUI
}