using UnityEngine;

using static UnityEngine.Mathf;

public class SpeedController : MonoBehaviour {
    public static float SpeedMultiplier => instance._speedMultiplier;
    
    static SpeedController instance;
    
    [SerializeField] float _maxSpeedMultiplier = 4;
    float _speedMultiplier = 1;

    float _now = 0;
    float _duration = 0;

    void Awake() {
        Debug.Assert(instance == null, $"[{GetType()}] It's a singleton! There is only one SpeedController can be", this);
        instance = this;
    }

    void OnEnable() {
        Timer.TimeStarted += OnTimeStarted;
    }

    void OnDisable() {
        Timer.TimeStarted -= OnTimeStarted;        
    }

    void OnTimeStarted(float duration) {
        _now = Time.time;
        _duration = duration;
    }

    void Update() {
        _speedMultiplier =  Max(_maxSpeedMultiplier * InverseLerp(_now, _now + _duration, Time.time), 1);
    }
    
    
}
