using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    [SerializeField] ScoreManager scoreManager = null;
    [SerializeField] ClockManager clockManager = null;
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

        yield return startWait;
    }

    IEnumerator RunGamePlaying() {
        clockManager.SetActive(true);

        while(!clockManager.ShouldEndGame()) {
            yield return null;
        }
    }

    IEnumerator RunGameEnding() {
        clockManager.SetActive(false);

        yield return endWait;
    }
}