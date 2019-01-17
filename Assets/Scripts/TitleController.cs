using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using TMPro;

public class TitleController : MonoBehaviour {
    [SerializeField] SceneFader sceneFader = null;
    [SerializeField] string gameSceneName = "Game";
    [SerializeField] string leaderboardsSceneName = "Leaderboards";
    [SerializeField] Toggle audioToggle = null;
    [SerializeField] TMP_Text audioText = null;

    void Start() {
        if(PlayerPrefs.HasKey("IsMuted") && PlayerPrefs.GetInt("IsMuted") == 1) {
            audioToggle.isOn = false;
            audioText.text = "Audio: Off";
            AudioListener.volume = 0;
        }
        else {
            audioToggle.isOn = true;
            audioText.text = "Audio: On";
            AudioListener.volume = 1;
        }
    }

    public void Play() {
        AnalyticsEvent.FirstInteraction();
        sceneFader.FadeToScene(gameSceneName);
    }

    public void Leaderboards() {
        sceneFader.FadeToScene(leaderboardsSceneName);
    }

    public void Audio(bool value) {
        PlayerPrefs.SetInt("IsMuted", value ? 0 : 1);
        PlayerPrefs.Save();
        audioText.text = value ? "Audio: On" : "Audio: Off";
        AudioListener.volume = value ? 1 : 0;
    }
}