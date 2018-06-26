using System;
using System.Collections;

using UnityEngine;

using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {
    [SerializeField] GameObject _commonBubble;
    [SerializeField] float _startSpeed = 5;
    [SerializeField] Vector2 _spawnCooldownRange;
    [SerializeField] Vector2 _bubbleWidthRange;
    [SerializeField] Color[] _colors;
    [SerializeField] float _bubbleBurstScore = 10;
    [SerializeField] float _offsetFromScreenBorder = 100;
    
    Coroutine _spawnCoroutine;
    
    const float START_Y_POSITION = -10;

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

    void OnTimeStarted(float duration) {
        _spawnCoroutine = StartCoroutine(Spawn());
    }

    void OnTimeEnded() {
        StopCoroutine(_spawnCoroutine);
    }

    IEnumerator Spawn() {
        while (true) {
            SpawnBubble();
            yield return new WaitForSeconds(GetSpawnCooldown());
        }
    }

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

    void OnDrawGizmos() {
        Func<Vector3, Vector3> CameraToWorld = Camera.main.ScreenToWorldPoint;
        
        var upLeftPos = CameraToWorld(new Vector3(_offsetFromScreenBorder, Screen.height, Camera.main.farClipPlane));
        var downLeftPos = CameraToWorld(new Vector3(_offsetFromScreenBorder, 0, Camera.main.farClipPlane));
        
        var upRightPos = CameraToWorld(new Vector3(Screen.width - _offsetFromScreenBorder, Screen.height, Camera.main.farClipPlane));
        var downRightPos = CameraToWorld(new Vector3(Screen.width - _offsetFromScreenBorder, 0, Camera.main.farClipPlane));

        Gizmos.DrawLine(upLeftPos, downLeftPos);
        Gizmos.DrawLine(upRightPos, downRightPos);

    }
}
