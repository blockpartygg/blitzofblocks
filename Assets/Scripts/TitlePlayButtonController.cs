using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePlayButtonController : MonoBehaviour {
    public void Play() {
        SceneManager.LoadScene("Game");
    }
}