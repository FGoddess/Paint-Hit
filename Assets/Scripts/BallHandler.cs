using System;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Transform _ballPrefab;

    [SerializeField] private float _force = 10f;

    private int _maxBalls = 5;
    private int _ballCount;

    public event Action BallsEnded;

    public void Initialize(int maxBalls)
    {
        _maxBalls = maxBalls;
        _ballCount = _maxBalls;
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
        var ball = Instantiate(_ballPrefab, transform.position, Quaternion.identity, transform);
        ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * _force, ForceMode.Impulse);

        --_ballCount;
        if (_ballCount < 1)
        {
            BallsEnded?.Invoke();

            _ballCount = _maxBalls;
        }
    }

}
