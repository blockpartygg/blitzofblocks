using UnityEngine;

public class ClockUpdater : MonoBehaviour {
    [SerializeField] ClockManager clockManager = null;

    void Update() {
        clockManager.UpdateSecondsRemaining(Time.deltaTime);
    }
}