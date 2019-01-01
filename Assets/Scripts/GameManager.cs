using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    [SerializeField] ScoreManager scoreManager = null;
    [SerializeField] ClockManager clockManager = null;
    [SerializeField] CursorSwapper cursorSwapper = null;
    [SerializeField] CursorRenderer cursorRenderer = null;
    [SerializeField] AnnouncementManager announcementManager = null;
    [SerializeField] FloatReference startDelay = null;
    [SerializeField] FloatReference endDelay = null;
    WaitForSeconds startWait;
    WaitForSeconds endWait;

    void Awake() {
        startWait = new WaitForSeconds(startDelay.Value);
        endWait = new WaitForSeconds(endDelay.Value);
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
        SceneManager.LoadScene("Results");
    }

    IEnumerator RunGameStarting() {
        clockManager.SetActive(false);
        clockManager.ResetSecondsRemaining();
        cursorSwapper.SetActive(false);
        announcementManager.ShowGameStarting();

        yield return startWait;
    }

    IEnumerator RunGamePlaying() {
        scoreManager.SetActive(true);
        clockManager.SetActive(true);
        cursorSwapper.SetActive(true);
        announcementManager.ShowGamePlaying();

        while(!clockManager.ShouldEndGame()) {
            yield return null;
        }
    }

    IEnumerator RunGameEnding() {
        scoreManager.SetActive(false);
        clockManager.SetActive(false);
        cursorSwapper.SetActive(false);
        cursorRenderer.SetVisible(false);
        announcementManager.ShowGameEnding();
        Time.timeScale = 0.1f;

        yield return endWait; // Note: this is multiplied by Time.timeScale
    }
}