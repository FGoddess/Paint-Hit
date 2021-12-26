using UnityEngine;

public class Circle : MonoBehaviour
{
    private bool _isNew = true;

    private CirclesManager _circlesManager;
    public CirclesManager CirclesManager { get => _circlesManager; set => _circlesManager = value; }

    private float _yToMove;
    private const float _YTOFALL = -2.5f;

    public void Initizlize()
    {
        _circlesManager.CircleAdded += OnNewCircleCreated;
    }

    private void OnDisable()
    {
        _circlesManager.CircleAdded -= OnNewCircleCreated;
    }

    private void OnNewCircleCreated()
    {
        MoveToY(_yToMove, false);
        _yToMove += _YTOFALL;

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
            nameof(iTween.EaseType), iTween.EaseType.spring,
            "time", 1f,
            "OnComplete", nameof(Rotate),
        }));
    }

    private void Rotate()
    {
        iTween.RotateBy(this.gameObject, iTween.Hash(new object[]
            {
                "y", _circlesManager.RotationsCount,
                "time", _circlesManager.RotationTime,
                nameof(iTween.EaseType), iTween.EaseType.easeInOutQuad,
                nameof(iTween.LoopType), iTween.LoopType.pingPong,
                "delay", 0.5f
            }));
    }
}
