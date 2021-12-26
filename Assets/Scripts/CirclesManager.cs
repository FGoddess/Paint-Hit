using System;
using UnityEngine;

public class CirclesManager : MonoBehaviour
{
    [SerializeField] private Transform _circlePrefab;

    [SerializeField] private Vector3 _circlePosition = new Vector3(0, 5, 20);

    [SerializeField] private BallHandler _ballHandler;

    [SerializeField] private Transform _container;

    private float _rotationTime;
    private float _rotationPerCircle;
    private float _minRotationSpeed;

    private float _rotationsCount;

    private Color[] _circleColors;
    private int _colorIndex = 0;

    public event Action<int> CircleAdded;

    public float RotationTime => _rotationTime;
    public float RotationsCount => _rotationsCount;


    public void Initialize(float rotationTime, float rotationPerCircle, float minRotationSpeed, float rotationsCount, Color[] circleColors)
    {
        _rotationTime = rotationTime;
        _rotationPerCircle = rotationPerCircle;
        _minRotationSpeed = minRotationSpeed;
        _rotationsCount = rotationsCount;
        this._circleColors = circleColors;

        AddCircle();
    }

    private void OnEnable()
    {
        _ballHandler.BallsEnded += AddCircle;
    }

    private void OnDisable()
    {
        _ballHandler.BallsEnded -= AddCircle;
    }

    private void AddCircle()
    {
        var circleObj = Instantiate(_circlePrefab, _circlePosition, Quaternion.identity, _container);
        var circle = circleObj.GetComponent<Circle>();

        circle.GetComponent<Circle>().CirclesManager = this;
        circle.GetComponent<Circle>().Initizlize(_circleColors[_colorIndex]);
        CircleAdded?.Invoke(_colorIndex);

        _colorIndex++;

        if (_colorIndex > _circleColors.Length - 1)
        {
            _colorIndex = 0;
        }

        if (_rotationTime > _minRotationSpeed)
        {
            _rotationTime -= _rotationPerCircle;
        }
    }
}
