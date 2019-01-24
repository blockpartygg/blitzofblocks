using UnityEngine;

public class CameraController : MonoBehaviour {
    Camera mainCamera;
    [SerializeField] IntReference boardColumns = null;
    [SerializeField] IntReference boardRows = null;

    void Awake() {
        mainCamera = GetComponent<Camera>();
    }

    void Start() {
        float constraint = Mathf.Max(boardColumns.Value, boardRows.Value / 2f);
        float size = constraint - 1f;

        mainCamera.orthographicSize = size;
        mainCamera.transform.position = new Vector3(0, boardRows.Value / 2f, -10);
    }
}
