using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class GameEvent : ScriptableObject {
    List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise() {
        for(int listenerIndex = listeners.Count - 1; listenerIndex >= 0; listenerIndex--) {
            listeners[listenerIndex].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener) {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener) {
        listeners.Remove(listener);
    }
}