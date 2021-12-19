using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    private bool _isRotating;
    private bool _isNew = true;

    private CirclesManager _circlesManager;
    public CirclesManager CirclesManager { get => _circlesManager; set => _circlesManager = value; }

    private float _yToMove;
    private const float _CONSTVALUE = -2.5f;

    public void Initizlize()
    {
        _circlesManager.CircleAdded += OnNewCircleCreated;
    }

    private void OnDisable()
    {
        _circlesManager.CircleAdded -= OnNewCircleCreated;
    }

    private void Start()
    {
        _yToMove = _CONSTVALUE;
        MoveToY(0, true);
    }

    private void OnNewCircleCreated()
    {
        MoveToY(_yToMove, false);
        _yToMove += _CONSTVALUE;

        if (!_isNew)
        {
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
        {
            _isNew = false;
        }
    }

    private void MoveToY(float yValue, bool boolValue)
    {
        iTween.MoveTo(gameObject, iTween.Hash(new object[]
        {
            "y", yValue,
            "easetype", iTween.EaseType.spring,
            "time", 1f,
            "OnComplete", nameof(SetBool),
            "OnCompleteParams", boolValue
        }));
    }

    private void SetBool(bool value)
    {
        _isRotating = value;
    }

    private void Update()
    {
        if (_isRotating)
        {
            transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        }
    }
}
