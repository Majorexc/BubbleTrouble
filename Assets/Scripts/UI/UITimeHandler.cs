using System.Collections;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UITimeHandler : MonoBehaviour {
	Text _text;
	Coroutine _timerCoroutine;
	
	void Awake() {
		_text = GetComponent<Text>();
	}

	void OnEnable() {
		Timer.TimeStarted += OnTimeStarted;
		Timer.TimeEnded += OnTimeEnded;
	}

	void OnDisable() {
		Timer.TimeStarted -= OnTimeStarted;
		Timer.TimeEnded -= OnTimeEnded;
	}

	void OnTimeStarted(float time) {
		_timerCoroutine = StartCoroutine(TimeCounter(time));
	}

	void OnTimeEnded() {
		StopCoroutine(_timerCoroutine);
	}

	IEnumerator TimeCounter(float time) {
		var now = Time.time;
		while (true) {
			var localTimer = Mathf.Round(time - (Time.time - now));
			_text.text = $"Time left: {localTimer}";
			yield return null;
		}
	}
}
