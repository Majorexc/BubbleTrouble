using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bubble : MonoBehaviour {
    float _startSpeed = 0;

    SpriteRenderer _spriteRenderer;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        Move();
    }

    public void Init(float startSpeed, float width, Color color) {
        transform.localScale *= width;
        _startSpeed = startSpeed * width;
        SetColor(color);
    }

    void Move() {
        transform.position += transform.up * _startSpeed * GetSpeedByTimeMultiplier() * Time.deltaTime;
    }

    float GetSpeedByTimeMultiplier() {
        return 1;
    }

    void SetColor(Color color) {
        _spriteRenderer.color = color;
    }
    
}
