using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates score visual
/// </summary>
[RequireComponent(typeof(Text))]
public class UIScoreHandler : MonoBehaviour {
    Text _text;
    float _score = 0;

    #region Unity Messages
    
    void Awake() {
        _text = GetComponent<Text>();
        UpdateScore();
    }

    void OnEnable() {
        Timer.TimeStarted += OnTimeStarted;
        Bubble.Burst += OnBubbleBurst;
    }

    void OnDisable() {
        Timer.TimeStarted -= OnTimeStarted;
        Bubble.Burst -= OnBubbleBurst;
    }

    void OnBubbleBurst(float score) {
        UpdateScore(score);
    }
    
    #endregion

    #region Event Handlers

    void OnTimeStarted(float time) {
        _score = 0;
        UpdateScore();
    }

    #endregion
    
    #region Private Methods

    void UpdateScore(float score = 0) {
        _score += score;
        _text.text = $"Score: {Mathf.Round(_score)}";
    }
    
    #endregion
}
