using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class TitlePlayButtonController : MonoBehaviour {
    [SerializeField] SceneFader sceneFader = null;

    public void Play() {
        AnalyticsEvent.FirstInteraction();
        sceneFader.FadeToScene("Game");
    }
}