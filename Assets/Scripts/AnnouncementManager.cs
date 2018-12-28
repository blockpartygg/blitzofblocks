using UnityEngine;
using TMPro;
using System.Collections;

public class AnnouncementManager : MonoBehaviour {
    [SerializeField] TMP_Text text = null;
    WaitForSeconds secondWait;
    
    void Awake() {
        secondWait = new WaitForSeconds(1f);
    }

    void Start() {
        StartCoroutine(RunCountdownSequence());
    }

    IEnumerator RunCountdownSequence() {
        text.enabled = true;

        yield return StartCoroutine(RunThreeCoroutine());
        yield return StartCoroutine(RunTwoCoroutine());
        yield return StartCoroutine(RunOneCoroutine());
        yield return StartCoroutine(RunGoCoroutine());

        text.enabled = false;
    }

    IEnumerator RunThreeCoroutine() {
        text.text = "3";

        yield return secondWait;
    }

    IEnumerator RunTwoCoroutine() {
        text.text = "2";

        yield return secondWait;
    }

    IEnumerator RunOneCoroutine() {
        text.text = "1";

        yield return secondWait;
    }

    IEnumerator RunGoCoroutine() {
        text.text = "Go!";

        yield return secondWait;
    }
}