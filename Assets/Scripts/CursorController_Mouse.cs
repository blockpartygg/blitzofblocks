using UnityEngine;

public class CursorController_Mouse : MonoBehaviour {
    [SerializeField] Camera mainCamera = null;
    [SerializeField] Cursor cursor = null;
    [SerializeField] CursorSelector selector = null;
    [SerializeField] CursorSwapper swapper = null;

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null && hit.collider.name.Contains("Block")) {
                Block block = hit.collider.gameObject.GetComponent<Block>();
                selector.Select(block);
            }
        }

        if(Input.GetMouseButtonUp(0)) {
            selector.Deselect();
        }

        if (cursor.SelectedBlock != null) {
            float leftEdge = cursor.SelectedBlock.transform.parent.position.x + cursor.SelectedBlock.Column - cursor.SelectedBlock.transform.localScale.x / 2;
            float rightEdge = cursor.SelectedBlock.transform.parent.position.x + cursor.SelectedBlock.Column + cursor.SelectedBlock.transform.localScale.x / 2;

            if(mainCamera.ScreenToWorldPoint(Input.mousePosition).x < leftEdge) {
                swapper.SwapLeft();
            }

            if(mainCamera.ScreenToWorldPoint(Input.mousePosition).x > rightEdge) {
                swapper.SwapRight();
            }
        }
    }
}