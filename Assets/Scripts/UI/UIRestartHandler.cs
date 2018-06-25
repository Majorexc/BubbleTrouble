using UnityEngine;

public class UIRestartHandler : MonoBehaviour {
    GameObject _button;
    
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

    void OnTimerStarted(float time) {
        _button.SetActive(false);
    }

    void OnTimerEnded() {
        _button.SetActive(true);        
    }
}
