using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private LevelData _levelData;
    [SerializeField] private CirclesManager _circlesManager;
    [SerializeField] private BallHandler _ballHandler;

    private void Awake()
    {
        _ballHandler.Initialize(_levelData.BallsCount);
        _circlesManager.Initialize(_levelData.RotationTime, _levelData.RotationPerCircle, _levelData.MinRotationSpeed, _levelData.RotationsCount);
    }
}
