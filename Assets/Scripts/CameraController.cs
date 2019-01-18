using UnityEngine;

public class CameraController : MonoBehaviour {
    Camera camera;
    [SerializeField] IntReference boardColumns = null;
    [SerializeField] IntReference boardRows = null;

    void Awake() {
        camera = GetComponent<Camera>();
    }

    void Start() {
        float constraint = Mathf.Max(boardColumns.Value, boardRows.Value / 2f);
        float size = constraint - 1f;

        camera.orthographicSize = size;
        camera.transform.position = new Vector3(0, boardRows.Value / 2f, -10);
    }
}
