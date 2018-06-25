using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIScoreHandler : MonoBehaviour {
    Text _text;
    float _score = 0;

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

    void OnTimeStarted(float time) {
        _score = 0;
        UpdateScore();
    }

    void UpdateScore(float score = 0) {
        _score += score;
        _text.text = $"Score: {Mathf.Round(_score)}";
    }
}
