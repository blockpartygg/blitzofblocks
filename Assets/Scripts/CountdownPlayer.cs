using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CountdownPlayer : MonoBehaviour {
    [SerializeField] ClockManager clockManager = null;
    [SerializeField] List<AudioCue> countdownCues = null;
    [SerializeField] AudioSource audioSource = null;

    void Start() {
        StartCoroutine(UpdateTimeRemaining());
    }

    IEnumerator UpdateTimeRemaining() {
        while (true) {
            TimeSpan timeRemaining = TimeSpan.FromSeconds(clockManager.SecondsRemaining);
            if(timeRemaining.Minutes == 0 && timeRemaining.Seconds <= 10 && timeRemaining.Seconds > 0) {
                countdownCues[timeRemaining.Seconds - 1].Play(audioSource);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
