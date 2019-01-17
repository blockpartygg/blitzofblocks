using UnityEngine;
using UnityEngine.Analytics;

public class TitleController : MonoBehaviour {
    [SerializeField] SceneFader sceneFader = null;
    [SerializeField] string gameSceneName = "Game";
    [SerializeField] string leaderboardsSceneName = "Leaderboards";

    public void Play() {
        AnalyticsEvent.FirstInteraction();
        sceneFader.FadeToScene(gameSceneName);
    }

    public void Leaderboards() {
        sceneFader.FadeToScene(leaderboardsSceneName);
    }
}