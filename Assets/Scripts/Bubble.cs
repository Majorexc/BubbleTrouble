using System;
using System.Collections;

using UnityEngine;

/// <summary>
/// Bubble logic
/// Moves, changes color and dies
/// Reacts to user's touch
/// </summary>
[RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Collider2D))]
public class Bubble : MonoBehaviour {
    public static event Action<float> Burst;     ///< Raises when bubble has been burst 
    
    float _startSpeed = 0;
    float _score;                                ///< Add this score when bubble burst
    Collider2D _collider;
    SpriteRenderer _spriteRenderer;
    
    const float DIE_DURATION_SECONDS = 0.3f;

    #region Unity Messages

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
    
    void OnMouseDown() {
        Burst?.Invoke(_score);
        Die();
    }

    #endregion

    #region Public Methods
    
    /// <summary>
    /// Initialize bubble
    /// </summary>
    /// <param name="startSpeed">Base speed of standart bubble</param>
    /// <param name="width">Width modifier</param>
    /// <param name="color">Start color</param>
    /// <param name="score">Standart score to add</param>
    public void Init(float startSpeed, float width, Color color, float score) {
        transform.localScale *= width;
        _startSpeed = startSpeed * (1 / width);
        _score = score * (1 / width);
        SetColor(color);
    }

    #endregion

    #region Private Methods
    
    /// <summary>
    /// Moves
    /// </summary>
    void Move() {
        transform.Translate(Vector3.up * _startSpeed * SpeedController.SpeedMultiplier * Time.deltaTime);
    }
    
    /// <summary>
    /// Changes color
    /// </summary>
    /// <param name="color"></param>
    void SetColor(Color color) {
        _spriteRenderer.material.SetColor("_Color", color);
    }
    
    void Die() {
        StartCoroutine(Dying());        
    }
    
    #endregion

    #region Event Handlers
    
    void OnTimeEnded() {
        Die();
    }
    
    #endregion

    #region Coroutines

    IEnumerator Dying() {
        // Prevent react to touch when bubble in dying progress
        _collider.enabled = false;
        
        var now = Time.time;
        while (Time.time < now + DIE_DURATION_SECONDS) {
            // just reduce size
            var t = Mathf.InverseLerp(now, now + DIE_DURATION_SECONDS, Time.time);
            transform.localScale *= 1 - t;
            
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
    
    #endregion
}
