using System.Collections;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates timer visual
/// </summary>
[RequireComponent(typeof(Text))]
public class UITimeHandler : MonoBehaviour {
	Text _text;
	Coroutine _timerCoroutine;
	
	#region Unity Messages
	
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
	
	#endregion

	#region Event Handlers

	
	void OnTimeStarted(float time) {
		_timerCoroutine = StartCoroutine(TimeCounter(time));
	}

	void OnTimeEnded() {
		StopCoroutine(_timerCoroutine);
	}
	
	#endregion

	#region Coroutines
	
	IEnumerator TimeCounter(float time) {
		var now = Time.time;
		while (true) {
			var localTimer = Mathf.Round(time - (Time.time - now));
			_text.text = $"Time left: {localTimer}";
			yield return null;
		}
	}
	
	#endregion
}
