using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardContinueButtonController : MonoBehaviour {
    [SerializeField] SceneFader sceneFader = null;

    public void Continue() {
        sceneFader.FadeToScene("Title");
    }
}