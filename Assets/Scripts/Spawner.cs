using System;
using System.Collections;

using UnityEngine;

using Random = UnityEngine.Random;

/// <summary>
/// Spawns bubbles
/// </summary>
public class Spawner : MonoBehaviour {
    [SerializeField] GameObject _commonBubble;            ///< Common bubble prefab
    [SerializeField] float _startSpeed = 5;               ///< Base speed of bubble 
    [SerializeField] Vector2 _spawnCooldownRange;         ///< Cooldown between spawn iteration
    [SerializeField] Vector2 _bubbleWidthRange;           ///< Spawn bubbles with this width range
    [SerializeField] Color[] _colors;                     ///< Spawn bubbles with this colors
    [SerializeField] float _bubbleBurstScore = 10;        ///< Common score of bubble to add
    [SerializeField] float _offsetFromScreenBorder = 100; ///< Spawn offset from screen by x axis
    
    Coroutine _spawnCoroutine;
    
    // Start spawn position by Y axis
    const float START_Y_POSITION = -10;
    
    #region Unity Messages

    void Awake() {
        Debug.Assert(_commonBubble != null, $"[{GetType()}] No one bubble prefab found", this);
        Debug.Assert(_colors != null && _colors.Length > 0, $"[{GetType()}] Need to set at least one color", this);
    }

    void OnEnable() {
        Timer.TimeStarted += OnTimeStarted;
        Timer.TimeEnded += OnTimeEnded;
    }

    void OnDisable() {
        Timer.TimeStarted -= OnTimeStarted;
        Timer.TimeEnded -= OnTimeEnded;
    }
    
    void OnDrawGizmos() {
        // Just draw limition lines for debug
        Func<Vector3, Vector3> CameraToWorld = Camera.main.ScreenToWorldPoint;
        
        var upLeftPos = CameraToWorld(new Vector3(_offsetFromScreenBorder, Screen.height, Camera.main.farClipPlane));
        var downLeftPos = CameraToWorld(new Vector3(_offsetFromScreenBorder, 0, Camera.main.farClipPlane));
        
        var upRightPos = CameraToWorld(new Vector3(Screen.width - _offsetFromScreenBorder, Screen.height, Camera.main.farClipPlane));
        var downRightPos = CameraToWorld(new Vector3(Screen.width - _offsetFromScreenBorder, 0, Camera.main.farClipPlane));

        Gizmos.DrawLine(upLeftPos, downLeftPos);
        Gizmos.DrawLine(upRightPos, downRightPos);
    }
    
    #endregion

    #region Event Handlers

    void OnTimeStarted(float duration) {
        _spawnCoroutine = StartCoroutine(Spawn());
    }

    void OnTimeEnded() {
        StopCoroutine(_spawnCoroutine);
    }
    
    #endregion

    #region Private Methods

    void SpawnBubble() {
        var bubbleWidth = GetRandomBubbleWidth();
        var bubbleGameObject = Instantiate(_commonBubble, transform);

        
        bubbleGameObject.transform.position = GetRandomPosition();
        bubbleGameObject.GetComponent<Bubble>().Init(_startSpeed, bubbleWidth, GetRandomColor(), _bubbleBurstScore);
    }
    
    Color GetRandomColor() {
        return _colors[Random.Range(0, _colors.Length)];
    }

    float GetRandomBubbleWidth() {
        return Random.Range(_bubbleWidthRange.x, _bubbleWidthRange.y);
    }

    float GetSpawnCooldown() {
        return Random.Range(_spawnCooldownRange.x, _spawnCooldownRange.y);
    }

    Vector3 GetRandomPosition() {
        var xPosInScreen = Random.Range(_offsetFromScreenBorder, Screen.width - _offsetFromScreenBorder);
        
        var spawnPositionInScreen = new Vector3(xPosInScreen,
                                                0,
                                                Camera.main.farClipPlane);
        
        var spawnPositionInWorld = Camera.main.ScreenToWorldPoint(spawnPositionInScreen);
        spawnPositionInWorld.y = START_Y_POSITION;

        return spawnPositionInWorld;
    }
    
    #endregion
    
    #region Private Coroutines

    IEnumerator Spawn() {
        while (true) {
            SpawnBubble();
            yield return new WaitForSeconds(GetSpawnCooldown());
        }
    }

   #endregion
}
