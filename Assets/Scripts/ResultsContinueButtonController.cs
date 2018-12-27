using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsContinueButtonController : MonoBehaviour {
    public void Continue() {
        SceneManager.LoadScene("Game");
    }
}