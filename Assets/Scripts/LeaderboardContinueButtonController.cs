using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardContinueButtonController : MonoBehaviour {
    [SerializeField] SceneFader sceneFader = null;
    [SerializeField] string titleSceneToLoad = "Title";

    public void Continue() {
        sceneFader.FadeToScene(titleSceneToLoad);
    }
}