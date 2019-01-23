using UnityEngine;

public class GameBackButtonController : MonoBehaviour {
    [SerializeField] SceneFader fader = null;
    [SerializeField] string sceneToLoad = "Title";

    public void Back() {
        fader.FadeToScene(sceneToLoad);
    }
}
