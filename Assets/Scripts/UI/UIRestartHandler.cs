using UnityEngine;

/// <summary>
/// Shows Restart button
/// </summary>
public class UIRestartHandler : MonoBehaviour {
    GameObject _button;

    #region Unity Messages
    
    void Awake() {
        _button = transform.GetChild(0).gameObject;
    }

    void OnEnable() {
        Timer.TimeStarted += OnTimerStarted;
        Timer.TimeEnded += OnTimerEnded;
    }

    void OnDisable() {
        Timer.TimeStarted -= OnTimerStarted;
        Timer.TimeEnded -= OnTimerEnded;
    }

    #endregion

    #region Event Handlers
    
    void OnTimerStarted(float time) {
        _button.SetActive(false);
    }

    void OnTimerEnded() {
        _button.SetActive(true);        
    }

    #endregion
}
