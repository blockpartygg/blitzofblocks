using UnityEngine;
using System.Runtime.InteropServices;

public class URLOpener : MonoBehaviour {
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")] static extern void OpenURLInNewTab(string url);
#endif

    public void Open(string url) {
#if UNITY_WEBGL && !UNITY_EDITOR
        OpenURLInNewTab(url);
#else
        Application.OpenURL(url);
#endif
    }
}