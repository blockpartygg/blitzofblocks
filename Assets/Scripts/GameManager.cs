using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
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
        StartCoroutine(RunGameLoop());
    }

    IEnumerator RunGameLoop() {
        yield return StartCoroutine(RunGameStarting());
        yield return StartCoroutine(RunGamePlaying());
        yield return StartCoroutine(RunGameEnding());

        Debug.Log("Load results");
    }

    IEnumerator RunGameStarting() {
        Debug.Log("Game starting");

        clockManager.SetActive(false);
        clockManager.ResetSecondsRemaining();

        yield return startWait;
    }

    IEnumerator RunGamePlaying() {
        Debug.Log("Game playing");

        clockManager.SetActive(true);

        while(!clockManager.ShouldEndGame()) {
            yield return null;
        }
    }

    IEnumerator RunGameEnding() {
        Debug.Log("Game ending");

        clockManager.SetActive(false);

        yield return endWait;
    }
}