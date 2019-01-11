using UnityEngine;
using System;

[CreateAssetMenu]
public class ClockManager : ScriptableObject {
    [SerializeField] FloatReference gameDuration = null;
    public bool IsActive;
    [SerializeField] float secondsRemaining = 0;
    public float SecondsRemaining {
        get { return secondsRemaining; }
    }

    void OnEnable() {
        IsActive = true;
        secondsRemaining = gameDuration.Value;
    }

    void OnDisable() {
        IsActive = false;
        secondsRemaining = 0;
    }

    public void SetActive(bool isActive) {
        IsActive = isActive;
    }

    public void ResetSecondsRemaining() {
        secondsRemaining = gameDuration.Value;
    }

    public void UpdateSecondsRemaining(float deltaTime) {
        if(IsActive) {
            secondsRemaining -= Time.deltaTime;

            if(secondsRemaining < 0) {
                IsActive = false;
                secondsRemaining = 0;
            }
        }
    }

    public bool ShouldEndGame() {
        return !IsActive;
    }
}