using System;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private float _force = 10f;
    [SerializeField] private CirclesManager _circlesManager;

    private int _maxBalls = 5;
    private int _ballCount;

    private Color[] _successColors;
    private Color[] _missColors;
    private int _colorIndex = 0;

    public event Action BallsEnded;

    public void Initialize(int maxBalls, Color[] successColors, Color[] missColors)
    {
        _maxBalls = maxBalls;
        _ballCount = _maxBalls;

        _successColors = successColors;
        _missColors = missColors;
    }

    private void OnEnable()
    {
        _circlesManager.CircleAdded += OnCircleAdded;
    }

    private void OnDisable()
    {
        _circlesManager.CircleAdded -= OnCircleAdded;
    }

    private void OnCircleAdded(int index)
    {
        _colorIndex = index;
        Debug.Log(_colorIndex);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowBall();
        }
    }

    private void ThrowBall()
    {
        var ball = ObjectPool.Instance.GetPooledObject();

        ball.GetComponent<Ball>().Initialize(_successColors[_colorIndex], _missColors[_colorIndex]);
        ball.transform.position = transform.position;
        ball.SetActive(true);
        ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * _force, ForceMode.Impulse);

        --_ballCount;
        if (_ballCount < 1)
        {
            BallsEnded?.Invoke();

            _ballCount = _maxBalls;
        }
    }

}
