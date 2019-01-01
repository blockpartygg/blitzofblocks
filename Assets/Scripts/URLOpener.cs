using UnityEngine;

public class URLOpener : MonoBehaviour {
    public void Open(string url) {
        Application.OpenURL(url);
    }
}