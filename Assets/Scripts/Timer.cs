using System;
using System.Collections;

using UnityEngine;

public class Timer : MonoBehaviour {
    [SerializeField] float _gameDurationSeconds = 20;
    
    #region Static

    public static event Action<float> TimeStarted;
    public static event Action TimeEnded;
    
    #endregion

    Coroutine _timerCoroutine;
    
    void Start() {
        StartTimer();
    }

    public void Restart() {
        StartTimer();
    }
    

    void StartTimer() {
        Debug.Assert(_timerCoroutine == null, $"[{GetType()}] Timer is already working", this);
        TimeStarted.Invoke(_gameDurationSeconds);
        _timerCoroutine = StartCoroutine(TimerProgress(_gameDurationSeconds));
    }

    IEnumerator TimerProgress(float duration) {
        yield return new WaitForSeconds(duration);
        TimeEnded?.Invoke();
        _timerCoroutine = null;
    }
    
}
