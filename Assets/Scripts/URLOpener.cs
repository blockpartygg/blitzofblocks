using UnityEngine;
using System.Runtime.InteropServices;

public class URLOpener : MonoBehaviour {
    [DllImport("__Internal")] static extern void OpenURL(string url);

    public void Open(string url) {
#if !UNITY_EDITOR && UNITY_WEBGL
        OpenURL(url);
#else
        Application.OpenURL(url);
#endif
    }
}