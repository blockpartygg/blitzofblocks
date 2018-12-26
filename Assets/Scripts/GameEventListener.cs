using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour {
    [SerializeField] GameEvent gameEvent = null;
    [SerializeField] UnityEvent response = null;

    void OnEnable() {
        gameEvent.RegisterListener(this);
    }

    void OnDisable() {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised() {
        response.Invoke();
    }
}