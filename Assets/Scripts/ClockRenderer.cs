using UnityEngine;
using TMPro;
using System;

public class ClockRenderer : MonoBehaviour {
	[SerializeField] ClockManager clockManager = null;
	[SerializeField] TMP_Text text = null;
	[SerializeField] StringReference stringFormat = null;

	void Update() {
		TimeSpan timeRemaining = TimeSpan.FromSeconds(clockManager.SecondsRemaining);
		
		text.text = string.Format(stringFormat.Value, timeRemaining.Minutes, timeRemaining.Seconds, timeRemaining.Milliseconds);
	}
}
