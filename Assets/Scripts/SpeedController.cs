using UnityEngine;

using static UnityEngine.Mathf;

/// <summary>
/// Controlls additional speed by time
/// </summary>
public class SpeedController : MonoBehaviour {
    #region Static
    
    /// Additional speed multiplier
    public static float SpeedMultiplier => _speedMultiplier;

    static float _speedMultiplier = 1;
    
    #endregion
    
    [SerializeField] float _maxSpeedMultiplier = 4;    // Speed multiplier will reach this value by the end of the game

    float _now = 0;
    float _duration = 0;

    #region Unity Messages
    
    void OnEnable() {
        Timer.TimeStarted += OnTimeStarted;
    }

    void OnDisable() {
        Timer.TimeStarted -= OnTimeStarted;        
    }
    
    void Update() {
        // Multiplier can not be less than 1 and greater than _maxSpeedMultiplier
        _speedMultiplier = Clamp(_maxSpeedMultiplier * InverseLerp(_now, _now + _duration, Time.time), 1, _maxSpeedMultiplier);
    }

    #endregion

    #region Event Handlers
    
    void OnTimeStarted(float duration) {
        _now = Time.time;
        _duration = duration;
    }

    #endregion
}
