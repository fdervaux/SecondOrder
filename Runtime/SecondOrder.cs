using UnityEngine;

/// <summary>
/// Class to hold the second order data.
/// </summary>
[System.Serializable]
public class SecondOrder<T>
{
    /// <summary>
    /// The second order data.
    /// </summary>
    [SerializeField]
    private SecondOrderData _data;

    private bool _isInit = false;
    private T _lastPosition;
    private T _targetPosition, _targetVelocity;

    /// <summary>
    /// The target position.
    /// </summary>
    public T targetPosition { get => _targetPosition; set => _targetPosition = value; }
    
    /// <summary>
    /// The target velocity.
    /// </summary>
    public T targetVelocity { get => _targetVelocity; set => _targetVelocity = value; }
    
    /// <summary>
    /// The last position.
    /// </summary>
    public T lastPosition { get => _lastPosition; set => _lastPosition = value; }
    
    /// <summary>
    /// Second order data.
    /// </summary>
    public SecondOrderData Data { get => _data; set => _data = value; }
    
    /// <summary>
    /// Indicates if the second order data is initialized.
    /// </summary>
    public bool IsInit { get => _isInit; set => _isInit = value; }

    /// <summary>
    /// Initializes the second order data with the given position.
    /// </summary>
    /// <param name="position"> The position to initialize with.</param>
    public void Init(T position)
    {
        _lastPosition = position;
        _targetPosition = position;
        _isInit = true;
    }
}
