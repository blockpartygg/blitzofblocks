using UnityEngine;

public class BoardRenderer : MonoBehaviour {
    [SerializeField] Transform backWallTransform = null;
    [SerializeField] Transform leftWallTransform = null;
    [SerializeField] Transform rightWallTransform = null;
    [SerializeField] Transform topWallTransform = null;
    [SerializeField] Transform bottomWallTransform = null;
    [SerializeField] Transform blockRoot = null;
    [SerializeField] Transform cursorRoot = null;
    [SerializeField] IntReference boardColumns = null;
    [SerializeField] IntReference boardRows = null;

    void Start() {
        backWallTransform.position = new Vector3(0, 0.5f * boardRows.Value, 0.5f);
        backWallTransform.localScale = new Vector3(boardColumns.Value, boardRows.Value, 0.1f);
        leftWallTransform.position = new Vector3(-0.5f * boardColumns.Value, 0.5f * boardRows.Value, -0.5f);
        leftWallTransform.localScale = new Vector3(0.1f, boardRows.Value, 2f);
        rightWallTransform.position = new Vector3(0.5f * boardColumns.Value, 0.5f * boardRows.Value, -0.5f);
        rightWallTransform.localScale = new Vector3(0.1f, boardRows.Value, 2f);
        topWallTransform.position = new Vector3(0, boardRows.Value, -0.5f);
        topWallTransform.localScale = new Vector3(boardColumns.Value, 0.1f, 2f);
        bottomWallTransform.position = new Vector3(0, 0, -0.5f);
        bottomWallTransform.localScale = new Vector3(boardColumns.Value, 0.1f, 2f);
        blockRoot.position = new Vector3(-0.5f * (boardColumns.Value - 1), 0, 0);
        cursorRoot.position = new Vector3(Mathf.Ceil(-0.5f * (boardColumns.Value - 1)), 0.5f, -0.5f);
    }
}