using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    [SerializeField] ScoreManager scoreManager = null;
    [SerializeField] ClockManager clockManager = null;
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

        SceneManager.LoadScene("Results");
    }

    IEnumerator RunGameStarting() {
        clockManager.SetActive(false);
        clockManager.ResetSecondsRemaining();
        announcementManager.ShowGameStarting();

        yield return startWait;
    }

    IEnumerator RunGamePlaying() {
        clockManager.SetActive(true);
        announcementManager.ShowGamePlaying();

        while(!clockManager.ShouldEndGame()) {
            yield return null;
        }
    }

    IEnumerator RunGameEnding() {
        clockManager.SetActive(false);
        announcementManager.ShowGameEnding();

        yield return endWait;
    }
}