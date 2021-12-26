using System.Collections;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private bool _isNew = true;

    private CirclesManager _circlesManager;
    public CirclesManager CirclesManager { get => _circlesManager; set => _circlesManager = value; }

    private float _yToMove;
    private const float _YTOFALL = -2.5f;

    private Color _successColor;

    public void Initizlize(Color successColor)
    {
        _circlesManager.CircleAdded += OnNewCircleCreated;
        _successColor = successColor;
    }

    private void OnDisable()
    {
        _circlesManager.CircleAdded -= OnNewCircleCreated;
    }

    private void OnNewCircleCreated(int index)
    {
        MoveToY(_yToMove, false);
        _yToMove += _YTOFALL;

        if (!_isNew)
        {
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.color = _successColor;
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
