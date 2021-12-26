using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int _ballsCount = 5;

    [SerializeField] private float _rotationTime = 6f;
    [SerializeField] private float _rotationPerCircle = 0.4f;
    [SerializeField] private float _minRotationSpeed = 4f;

    [SerializeField] private float _rotationsCount = 2f;

    [SerializeField] private float _hitsForNextCircle = 3;

    [Space]
    [SerializeField] private Color[] _successColors;
    [SerializeField] private Color[] _missColors;

    public int BallsCount => _ballsCount;

    public float RotationTime => _rotationTime;
    public float RotationPerCircle => _rotationPerCircle;
    public float MinRotationSpeed => _minRotationSpeed;

    public float RotationsCount => _rotationsCount;

    public float HitsForNextCircle => _hitsForNextCircle;

    public Color[] SuccessColors => _successColors;
    public Color[] MissColors => _missColors;
}
