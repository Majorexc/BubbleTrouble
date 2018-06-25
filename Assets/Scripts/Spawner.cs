using System.Collections;

using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] GameObject _commonBubble;
    [SerializeField] float _startSpeed = 5;
    [SerializeField] Vector2 _spawnCooldownRange;
    [SerializeField] Vector2 _bubbleWidthRange;
    [SerializeField] Color[] _colors;
    [SerializeField] float _bubbleBurstScore = 10;
    
    Coroutine _spawnCoroutine;
    
    const float START_HEIGHT_POSITION = -10;
    const float OFFSET_MULTIPLIER = 6;

    void Awake() {
        Debug.Assert(_commonBubble != null, "[Spawner] No one bubble prefab found", this);
        Debug.Assert(_colors != null && _colors.Length > 0, "[Spawner] Need to set at least one color", this);
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
        var spawnPositionInScreen = new Vector3(GetRandomWidthPositionInScreen(),
                                                0, 
                                                Camera.main.farClipPlane);

        var bubbleWidth = GetRandomBubbleWidth();
        var bubbleGameObject = Instantiate(_commonBubble, transform);

        // Simple hack to prevent out of bounds bubble spawning
        var bubbleSize = bubbleGameObject.GetComponent<SpriteRenderer>().size.x * bubbleWidth * OFFSET_MULTIPLIER;
        spawnPositionInScreen.x = Mathf.Clamp(spawnPositionInScreen.x, bubbleSize, Screen.width - (bubbleSize));
        
        var spawnPositionInWorld = Camera.main.ScreenToWorldPoint(spawnPositionInScreen);
        spawnPositionInWorld.y = START_HEIGHT_POSITION;
        bubbleGameObject.transform.position = spawnPositionInWorld;
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

    float GetRandomWidthPositionInScreen() {
        return Random.Range(0, Screen.width);
    }
    
}
