using UnityEngine;

public class CursorSelector : MonoBehaviour {
    [SerializeField] Cursor cursor = null;
    [SerializeField] IntReference boardRows = null;

    public void Select(Block block) {
        if(block.State == BlockState.Idle || block.State == BlockState.Sliding || block.State == BlockState.Empty && block.Row >= 0 && block.Row <= boardRows.Value - 1) {
            cursor.SelectedBlock = block;
        }
    }

    public void Deselect() {
        cursor.SelectedBlock = null;
    }
}
