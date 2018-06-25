using System;
using System.Collections;

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Collider2D))]
public class Bubble : MonoBehaviour {
    public static event Action<float> Burst;

    Collider2D _collider;
    
    float _startSpeed = 0;
    float _score;
    
    SpriteRenderer _spriteRenderer;
    float _dieDurationSeconds = 0.3f;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    void OnEnable() {
        Timer.TimeEnded += OnTimeEnded;
    }

    void OnDisable() {
        Timer.TimeEnded -= OnTimeEnded;        
    }

    void Update() {
        Move();
    }

    public void Init(float startSpeed, float width, Color color, float score) {
        transform.localScale *= width;
        _startSpeed = startSpeed * (1 / width);
        _score = score * (1 / width);
        SetColor(color);
    }

    void Move() {
        transform.Translate(Vector3.up * _startSpeed * GetSpeedByTimeMultiplier() * Time.deltaTime);
    }

    float GetSpeedByTimeMultiplier() {
        return 1;
    }

    void SetColor(Color color) {
        _spriteRenderer.color = color;
    }

    void OnMouseDown() {
        Burst?.Invoke(_score);
        StartCoroutine(Die());
    }

    void OnTimeEnded() {
        StartCoroutine(Die());
    }

    IEnumerator Die() {
        _collider.enabled = false;
        var now = Time.time;
        while (Time.time < now + _dieDurationSeconds) {
            yield return new WaitForEndOfFrame();
            var t = Mathf.InverseLerp(now, now + _dieDurationSeconds, Time.time);
            transform.localScale *= 1 - t;
        }
        Destroy(gameObject);
    }
    
}
