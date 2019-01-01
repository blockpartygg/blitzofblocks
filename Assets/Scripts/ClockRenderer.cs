using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class ClockRenderer : MonoBehaviour {
	[SerializeField] ClockManager clockManager = null;
	[SerializeField] TMP_Text text = null;
	[SerializeField] StringReference stringFormat = null;

	void Start() {
		StartCoroutine(UpdateTimeRemaining());
	}

	IEnumerator UpdateTimeRemaining() {
		while(true) {
			TimeSpan timeRemaining = TimeSpan.FromSeconds(clockManager.SecondsRemaining);
			text.text = string.Format(stringFormat.Value, timeRemaining.Minutes, timeRemaining.Seconds);
			yield return new WaitForSeconds(1f);
		}
	}
}
