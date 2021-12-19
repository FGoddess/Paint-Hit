using System;
using UnityEngine;

public class CirclesManager : MonoBehaviour
{
    [SerializeField] private Transform _circlePrefab;

    [SerializeField] private Vector3 _circlePosition = new Vector3(0, 5, 20);

    [SerializeField] private BallHandler _ballHandler;

    public event Action CircleAdded;

    private void OnEnable()
    {
        _ballHandler.BallsEnded += AddCircle;
    }

    private void OnDisable()
    {
        _ballHandler.BallsEnded -= AddCircle;
    }

    private void Start()
    {
        AddCircle();
    }

    private void AddCircle()
    {
        var circleObj = Instantiate(_circlePrefab, _circlePosition, Quaternion.identity);
        var circle = circleObj.GetComponent<Circle>();

        circle.GetComponent<Circle>().CirclesManager = this;
        circle.GetComponent<Circle>().Initizlize();

        CircleAdded?.Invoke();
    }
}
