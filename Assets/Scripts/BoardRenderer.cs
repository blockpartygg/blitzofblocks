using UnityEngine;

public class BoardRenderer : MonoBehaviour {
    [SerializeField] Transform spriteTransform = null;
    [SerializeField] Transform spriteMaskTransform = null;
    [SerializeField] Transform blockRoot = null;
    [SerializeField] Transform cursorRoot = null;
    [SerializeField] Transform panelRoot = null;
    [SerializeField] IntReference boardColumns = null;
    [SerializeField] IntReference boardRows = null;

    void Start() {
        spriteTransform.position = new Vector3(0, 0.5f * boardRows.Value, 0.5f);
        spriteTransform.localScale = new Vector3(boardColumns.Value, boardRows.Value, 1f);

        spriteMaskTransform.position = new Vector3(0, 0.5f * boardRows.Value, 0.5f);
        spriteMaskTransform.localScale = new Vector3(boardColumns.Value, boardRows.Value, 1f);

        blockRoot.position = new Vector3(-0.5f * (boardColumns.Value - 1), 0, 0);
        cursorRoot.position = new Vector3(Mathf.Ceil(-0.5f * (boardColumns.Value - 1)), 0.5f, -0.1f);
        panelRoot.position = new Vector3(-0.5f * (boardColumns.Value - 1), 0, -0.5f);
    }
}