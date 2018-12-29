using UnityEngine;

public class PanelTest : MonoBehaviour {
    [SerializeField] PanelManager panelManager = null;
    int value;

    void OnGUI() {
        GUI.Box(new Rect(0, 0, 100, 100), "Panel Test");

        GUI.Label(new Rect(5, 25, 60, 25), "Value:");
        value = int.Parse(GUI.TextField(new Rect(45, 25, 50, 25), value.ToString()));
        if(GUI.Button(new Rect(5, 50, 90, 25), "Play Combo")) {
            panelManager.Panels[0, 0].Play(PanelType.Combo, value);
        }
        if(GUI.Button(new Rect(5, 75, 90, 25), "Play Chain")) {
            panelManager.Panels[0, 0].Play(PanelType.Chain, value);
        }
    }
}