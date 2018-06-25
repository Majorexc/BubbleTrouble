using System;
using System.Collections;

using UnityEngine;

public class Timer : MonoBehaviour {
    #region Static

    public static event Action<float> TimeStarted;
    public static event Action TimeEnded;
    
    #endregion

    [SerializeField] float _gameDurationSeconds = 10;

    Coroutine _timerCoroutine;
    
    void Start() {
        StartTimer();
    }

    void StartTimer() {
        Debug.Assert(_timerCoroutine != null, $"[{GetType()}] Timer is already working", this);
        TimeStarted.Invoke(_gameDurationSeconds);
        _timerCoroutine = StartCoroutine(TimerProgress(_gameDurationSeconds));
    }

    IEnumerator TimerProgress(float duration) {
        yield return new WaitForSeconds(duration);
        TimeEnded?.Invoke();
        _timerCoroutine = null;
    }
    
}
