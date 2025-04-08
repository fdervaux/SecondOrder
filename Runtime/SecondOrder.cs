
using UnityEngine;

[System.Serializable]
public class SecondOrder<T>
{
    [SerializeField]
    private SecondOrderData _data;

    private bool _isInit = false;

    private T _lastPosition;
    private T _targetPosition, _targetVelocity;

    public T targetPosition { get => _targetPosition; set => _targetPosition = value; }
    public T targetVelocity { get => _targetVelocity; set => _targetVelocity = value; }
    public T lastPosition { get => _lastPosition; set => _lastPosition = value; }
    public SecondOrderData Data { get => _data; set => _data = value; }
    public bool IsInit { get => _isInit; set => _isInit = value; }

    public void Init(T position)
    {
        _lastPosition = position;
        _targetPosition = position;
        _isInit = true;
    }
}
