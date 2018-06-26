using System;
using System.Collections;

using UnityEngine;

/// <summary>
/// Time controller
/// </summary>
public class Timer : MonoBehaviour {
    // How long will last the game 
    [SerializeField] float _gameDurationSeconds = 20;
    
    #region Static

    public static event Action<float> TimeStarted;       ///< Raises when the game starts
    public static event Action TimeEnded;                ///< Raises when the game ends
    
    #endregion

    Coroutine _timerCoroutine;
    
    #region Unity Messages
    
    void Start() {
        StartTimer();
    }
    
    #endregion

    #region Public Methods
    /// <summary>
    /// Uses for Restart button in UI panel
    /// </summary>
    public void Restart() {
        StartTimer();
    }
    
    #endregion

    #region Private Methods
        
    void StartTimer() {
        Debug.Assert(_timerCoroutine == null, $"[{GetType()}] Timer is already working", this);
    
        TimeStarted?.Invoke(_gameDurationSeconds);
        _timerCoroutine = StartCoroutine(TimerProgress(_gameDurationSeconds));
    }
    
    #endregion

    #region Coroutines

    IEnumerator TimerProgress(float duration) {
        yield return new WaitForSeconds(duration);
        TimeEnded?.Invoke();
        _timerCoroutine = null;
    }
    
    #endregion
}
