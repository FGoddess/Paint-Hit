using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectToPool
{
    [SerializeField] private PoolType _poolType;
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private int _amountToPool;
    private List<GameObject> _pooledObjects;

    public PoolType PoolType => _poolType;
    public GameObject ObjectPrefab => _objectPrefab;
    public int AmountToPool => _amountToPool;
    public List<GameObject> PooledObjects { get => _pooledObjects; set => _pooledObjects = value; }
}