using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class AnnouncementManager : MonoBehaviour {
    [SerializeField] TMP_Text text = null;
    WaitForSeconds oneSecondWait;
    WaitForSeconds threeSecondWait;
    
    void Awake() {
        oneSecondWait = new WaitForSeconds(1f);
        threeSecondWait = new WaitForSeconds(3f);
    }

    public void ShowGameStarting() {
        StartCoroutine(RunCountdownSequence());
    }

    IEnumerator RunCountdownSequence() {
        text.enabled = true;

        yield return StartCoroutine(RunModeObjectiveCoroutine());
        yield return StartCoroutine(RunThreeCoroutine());
        yield return StartCoroutine(RunTwoCoroutine());
        yield return StartCoroutine(RunOneCoroutine());
    }

    IEnumerator RunModeObjectiveCoroutine() {
        text.text = "Time Attack\n<size=64>Score as many points in 2 minutes as possible</size>";
        text.transform.DOScale(0, 0.25f).SetDelay(2);

        yield return threeSecondWait;
    }

    IEnumerator RunThreeCoroutine() {
        text.text = "3";
        text.transform.DOScale(1, 0.25f);
        text.transform.DOScale(0, 0.25f).SetDelay(0.5f);

        yield return oneSecondWait;
    }

    IEnumerator RunTwoCoroutine() {
        text.text = "2";
        text.transform.DOScale(1, 0.25f);
        text.transform.DOScale(0, 0.25f).SetDelay(0.5f);

        yield return oneSecondWait;
    }

    IEnumerator RunOneCoroutine() {
        text.text = "1";
        text.transform.DOScale(1, 0.25f);
        text.transform.DOScale(0, 0.25f).SetDelay(0.5f);

        yield return oneSecondWait;
    }

    public void ShowGamePlaying() {
        StartCoroutine(RunGoCoroutine());
    }

    IEnumerator RunGoCoroutine() {
        text.text = "Go!";
        text.transform.DOScale(2, 0.25f);
        text.transform.DOScale(0, 0.75f).SetDelay(1f);
        text.DOColor(Color.clear, 0.75f).SetDelay(1f);

        yield return new WaitForSeconds(2f);

        text.enabled = false;
    }

    public void ShowGameEnding() {
        text.enabled = true;
        text.text = "Time's up!";
        text.color = Color.white;
        text.transform.DOScale(2, 0.25f * Time.timeScale);
    }
}