using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    [SerializeField] ScoreManager scoreManager = null;
    [SerializeField] ClockManager clockManager = null;
    [SerializeField] CursorSwapper cursorSwapper = null;
    [SerializeField] CursorRenderer cursorRenderer = null;
    [SerializeField] AnnouncementManager announcementManager = null;
    [SerializeField] SceneFader sceneFader = null;
    [SerializeField] AudioCue musicCue = null;
    [SerializeField] FloatReference startDelay = null;
    [SerializeField] FloatReference endDelay = null;
    [SerializeField] string nextSceneToLoad = "Results";
    WaitForSeconds startWait;
    WaitForSeconds endWait;
    MusicPlayer musicPlayer;

    void Awake() {
        startWait = new WaitForSeconds(startDelay.Value);
        endWait = new WaitForSeconds(endDelay.Value);
        musicPlayer = FindObjectOfType<MusicPlayer>();
    }

    void Start() {
        scoreManager.Reset();

        StartCoroutine(RunGameLoop());
    }

    IEnumerator RunGameLoop() {
        yield return StartCoroutine(RunGameStarting());
        yield return StartCoroutine(RunGamePlaying());
        yield return StartCoroutine(RunGameEnding());

        Time.timeScale = 1f;
        sceneFader.FadeToScene(nextSceneToLoad);
    }

    IEnumerator RunGameStarting() {
        clockManager.SetActive(false);
        clockManager.ResetSecondsRemaining();
        cursorSwapper.SetActive(false);
        announcementManager.ShowGameStarting();
        AnalyticsEvent.GameStart();

        yield return startWait;
    }

    IEnumerator RunGamePlaying() {
        scoreManager.SetActive(true);
        clockManager.SetActive(true);
        cursorSwapper.SetActive(true);
        announcementManager.ShowGamePlaying();
        musicPlayer.SetMusic(musicCue);

        while (!clockManager.ShouldEndGame()) {
            yield return null;
        }
    }

    IEnumerator RunGameEnding() {
        scoreManager.SetActive(false);
        clockManager.SetActive(false);
        cursorSwapper.SetActive(false);
        cursorRenderer.SetVisible(false);
        Time.timeScale = 0.1f;
        announcementManager.ShowGameEnding();
        musicPlayer.Stop();

        Dictionary<string, object> eventData;
        eventData = new Dictionary<string, object>();
        eventData.Add("Score", scoreManager.Points);
        eventData.Add("Blocks Matched", scoreManager.BlocksMatched);
        AnalyticsEvent.GameOver("Time Attack", eventData);

        yield return endWait; // Note: this is multiplied by Time.timeScale
    }
}