using UnityEngine;

public class PanelManager : MonoBehaviour {
	public Panel[,] Panels;
    [SerializeField] Panel panelPrefab = null;
    [SerializeField] Transform parentTransform = null;
    // public BoardRaiser BoardRaiser;
    // public FloatReference RaiseDuration;
    [SerializeField] IntReference boardColumns = null, boardRows = null;
    [SerializeField] StringReference panelName = null;

    Vector3 initialPosition;

    void Awake() {
        Panels = new Panel[boardColumns.Value, boardRows.Value];

        for(int row = 0; row < boardRows.Value; row++) {
            for(int column = 0; column < boardColumns.Value; column++) {
                Panels[column, row] = Instantiate(panelPrefab, parentTransform);
                Panels[column, row].name = string.Format(panelName.Value, column , row);
                Panels[column, row].Column = column;
                Panels[column, row].Row = row;
            }
        }
    }

    void Update() {
        // Vector3 raiseTranslation = initialPosition + new Vector3(0, BoardRaiser.Elapsed / RaiseDuration.Value);

		// transform.position = raiseTranslation;
    }
}