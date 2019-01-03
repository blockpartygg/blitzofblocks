using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsContinueButtonController : MonoBehaviour {
    [SerializeField] SceneFader sceneFader = null;

    public void Continue() {
        sceneFader.FadeToScene("Title");
    }
}